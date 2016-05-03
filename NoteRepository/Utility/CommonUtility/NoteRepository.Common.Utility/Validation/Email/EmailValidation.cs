using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace NoteRepository.Common.Utility.Validation.Email
{
    /// <summary>
    /// The class gets a few way to check a email string is valid and reachable
    /// </summary>
    public static class EmailValidation
    {
        #region private enumerator

        private enum SMTPResponse
        {
            ConnectSuccess = 220,
            GenericSuccess = 250,
            DataSuccess = 354,
            QuitSuccess = 221
        }

        #endregion private enumerator

        #region public methods

        /// <summary>
        /// Determines whether the specified email string is email.
        /// </summary>
        /// <param name="emailString">The email string.</param>
        /// <returns>
        ///   <c>true</c> if the specified email string is email; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmail(string emailString)
        {
            if (string.IsNullOrEmpty(emailString))
            {
                return false;
            }

            const string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                    @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                    @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            var re = new Regex(strRegex);
            return re.IsMatch(emailString);
        }

        /// <summary>
        /// Determines whether the email server is reachable for the specified email string.
        /// </summary>
        /// <param name="emailString">The email string.</param>
        /// <returns>
        ///   <c>true</c> if the email is reachable; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmailReachable(string emailString)
        {
            if (!IsEmail(emailString))
            {
                return false;
            }

            var host = (emailString.Split('@'));
            var hostname = host[1];

            try
            {
                //var mxRecs = GetMXRecords(hostname);
                var mxRecs = new[] { "mail.hadrian-inc.com" };
                if (mxRecs.Length == 0)
                {
                    return false;
                }

                var result = false;
                foreach (var mxRec in mxRecs)
                {
                    var iphst = Dns.GetHostEntry(mxRec);
                    var endPt = new IPEndPoint(iphst.AddressList[0], 25);
                    var s = new Socket(endPt.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    s.Connect(endPt);

                    //Attempting to connect
                    if (!CheckResponse(s, SMTPResponse.ConnectSuccess))
                    {
                        s.Close();
                        continue;
                    }

                    //HELO server
                    Senddata(s, string.Format(CultureInfo.InvariantCulture, "HELO {0}\r\n", Dns.GetHostName()));
                    if (!CheckResponse(s, SMTPResponse.GenericSuccess))
                    {
                        s.Close();
                        continue;
                    }

                    //Identify yourself
                    //Servers may resolve your domain and check whether
                    //you are listed in BlackLists etc.
                    Senddata(s, string.Format(CultureInfo.InvariantCulture, "MAIL FROM: <{0}>\r\n", "jfangtesting@gmail.com"));
                    if (!CheckResponse(s, SMTPResponse.GenericSuccess))
                    {
                        s.Close();
                        continue;
                    }

                    //Attempt Delivery (I can use VRFY, but most
                    //SMTP servers only disable it for security reasons)
                    Senddata(s, string.Format(CultureInfo.InvariantCulture, "RCPT TO: <{0}>\r\n", emailString));
                    if (!CheckResponse(s, SMTPResponse.GenericSuccess))
                    {
                        s.Close();
                        continue;
                    }

                    Senddata(s, string.Format(CultureInfo.InvariantCulture, "DATA\r\n hello Jack this is from your testing app!:)\r\n"));
                    if (!CheckResponse(s, SMTPResponse.GenericSuccess))
                    {
                        s.Close();
                        continue;
                    }
                    result = true;
                    break;
                }

                return result;
            }
            catch
            {
                // for whatever error we got, just return false to indicated bad domain name
                return false;
            }
        }

        #endregion public methods

        #region private methods

        /// <summary>
        /// Checks the response of SMTP server.
        /// </summary>
        /// <param name="s">The socket which link to server.</param>
        /// <param name="responseExpected">The response result expected.</param>
        /// <returns>True if the response equal to responseExpected, otherwise false</returns>
        private static bool CheckResponse(Socket s, SMTPResponse responseExpected)
        {
            var bytes = new byte[1024];
            while (s.Available == 0)
            {
                System.Threading.Thread.Sleep(100);
            }

            s.Receive(bytes, 0, s.Available, SocketFlags.None);
            var responseStr = Encoding.ASCII.GetString(bytes);
            var response = Convert.ToInt32(responseStr.Substring(0, 3));

            return response == (int)responseExpected;
        }

        /// <summary>
        /// Send data server via specified socket.
        /// </summary>
        /// <param name="s">The socket which link to server.</param>
        /// <param name="message">The message to be send.</param>
        private static void Senddata(Socket s, string message)
        {
            byte[] msg = Encoding.ASCII.GetBytes(message);
            s.Send(msg, 0, msg.Length, SocketFlags.None);
        }

        [DllImport("dnsapi", EntryPoint = "DnsQuery_W", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        private static extern int DnsQuery([MarshalAs(UnmanagedType.VBByRefStr)]ref string pszName, DnsQueryTypes wType, DnsQueryOptions options, int aipServers, ref IntPtr ppQueryResults, int pReserved);

        [DllImport("dnsapi", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern void DnsRecordListFree(IntPtr pRecordList, int freeType);

        /// <summary>
        /// Gets the Mail eXchange server records.
        /// </summary>
        /// <param name="domain">The domain name of the MX server.</param>
        /// <returns>A string array of all MX records found from server</returns>
        private static string[] GetMXRecords(string domain)
        {
            var ptr1 = IntPtr.Zero;

            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                throw new NotSupportedException();
            }

            var list1 = new ArrayList();
            try
            {
                int num1 = DnsQuery(ref domain, DnsQueryTypes.DnsTypeMX, DnsQueryOptions.DnsQueryBypassCache, 0, ref ptr1, 0);
                if (num1 != 0)
                {
                    if (num1 == 9003)
                    {
                        list1.Add("DNS record does not exist");
                    }
                    else
                    {
                        throw new Win32Exception(num1);
                    }
                }

                MXRecord recMx;
                IntPtr ptr2;
                for (ptr2 = ptr1; !ptr2.Equals(IntPtr.Zero); ptr2 = recMx.pNext)
                {
                    recMx = (MXRecord)Marshal.PtrToStructure(ptr2, typeof(MXRecord));
                    if (recMx.wType == 15)
                    {
                        var text1 = Marshal.PtrToStringAuto(recMx.pNameExchange);
                        if (text1 != null)
                        {
                            list1.Add(text1);
                        }
                    }
                }
            }
            finally
            {
                DnsRecordListFree(ptr1, 0);
            }
            return (string[])list1.ToArray(typeof(string));
        }

        #endregion private methods
    }
}
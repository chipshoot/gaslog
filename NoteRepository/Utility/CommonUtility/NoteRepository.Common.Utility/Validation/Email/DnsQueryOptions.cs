namespace NoteRepository.Common.Utility.Validation.Email
{
    internal enum DnsQueryOptions
    {
        DnsQueryAcceptTruncatedResponse = 1,

        DnsQueryBypassCache = 8,
        
        DnsQueryDontResetTtlValues = 0x100000,
        
        DnsQueryNoHostsFile = 0x40,
        
        DnsQueryNoLocalName = 0x20,
        
        DnsQueryNoNetbt = 0x80,
        
        DnsQueryNoRecursion = 4,
        
        DnsQueryNoWireQuery = 0x10,
        
        DnsQueryReserved = -16777216,
        
        DnsQueryReturnMessage = 0x200,
        
        DnsQueryStandard = 0,
        
        DnsQueryTreatAsFqdn = 0x1000,
        
        DnsQueryUseTcpOnly = 2,
        
        DnsQueryWireOnly = 0x100
    }
}
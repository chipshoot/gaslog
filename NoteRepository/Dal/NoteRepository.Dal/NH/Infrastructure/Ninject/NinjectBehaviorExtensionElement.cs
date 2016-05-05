using System;
using System.ServiceModel.Configuration;

namespace NoteRepository.Dal.NH.Infrastructure.Ninject
{
    public class NinjectBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType => typeof(NinjectInstanceProvider);

        protected override object CreateBehavior()
        {
            return new NinjectBehaviorAttribute();
        }
    }
}
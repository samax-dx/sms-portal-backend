using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmsGateway
{
    public class SmsProviderSMPP : ISmsProvider
    {
        public SmsProviderSMPP() { }
        public async Task<string> SendSms(SmsTask smsTask)
        {
            throw new NotImplementedException();
        }
    }
}

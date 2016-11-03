using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class OrganizationModel
    {
        public string Org_Code { get; set; }

        public string EncryptKeyName { get; set; }

        public string CheckSignPubKeyPath { get; set; }

        public string SignPriKeyPath { get; set; }
    }
}

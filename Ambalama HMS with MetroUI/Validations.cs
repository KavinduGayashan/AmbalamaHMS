using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambalama_HMS_with_MetroUI
{
    class Validations
    {
        public Boolean ValidateNIC(String nic)
        {
            if (nic != "" && nic.Length == 10)
            {
                for (int i = 0; i < nic.Length - 1; i++)
                {
                    if (!Char.IsDigit(nic[i]))
                    {
                        return false;
                    }
                }
            }
            else
                return false;


            return true;
        }

        public Boolean ValidateName(String name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                for (int i = 0; i < name.Length; i++)
                {
                    if (Char.IsLetter(name[i]))
                    {
                        continue;
                    }
                    else if (Char.IsWhiteSpace(name[i]) && i > 1)
                    {
                        continue;
                    }
                    else
                        return false;
                }
            }
            else
                return false;

            return true;
        }

        public Boolean ValidatePhone(String phone)
        {
            if (phone.Length == 10)
            {
                for(int i=0;i<phone.Length;i++)
                {
                    if (!Char.IsNumber(phone[i]))
                        return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}

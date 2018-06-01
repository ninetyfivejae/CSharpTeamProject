using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignUpProgram
{
    class MemberVO
    {
        private string MemberName;                      //회원이름
        private string MemberFirstResidentNumber;       //회원주민등록번호 앞자리, 생년월일
        private string MemberSecondResidentNumber;      //회원주민등록번호 뒷자리
        private string MemberUsername;                  //회원 아이디
        private string MemberEmail;                     //회원 이메일
        private string MemberPassword;                  //회원 비밀번호
        private string MemberPhoneNumber;               //회원 전화번호
        private string MemberAddress;                   //회원 주소, 도로명주소 및 건물번호
        private string MemberSpecificAddress;           //회원 상세주소

        public MemberVO() { }

        public MemberVO(string MemberName, string MemberFirstResidentNumber, string MemberSecondResidentNumber, string MemberUsername, string MemberEmail, string MemberPassword, string MemberPhoneNumber, string MemberAddress, string MemberSpecificAddress)
        {
            this.MemberName = MemberName;
            this.MemberFirstResidentNumber = MemberFirstResidentNumber;
            this.MemberSecondResidentNumber = MemberSecondResidentNumber;
            this.MemberUsername = MemberUsername;
            this.MemberEmail = MemberEmail;
            this.MemberPassword = MemberPassword;
            this.MemberPhoneNumber = MemberPhoneNumber;
            this.MemberAddress = MemberAddress;
            this.MemberSpecificAddress = MemberSpecificAddress;
        }

        public string MemberNAME
        {
            get { return MemberName; }
            set { MemberName = value; }
        }

        public string MemberFIRSTRESIDENTNUMBER
        {
            get { return MemberFirstResidentNumber; }
            set { MemberFirstResidentNumber = value; }
        }

        public string MemberSECONDRESIDENTNUMBER
        {
            get { return MemberSecondResidentNumber; }
            set { MemberSecondResidentNumber = value; }
        }

        public string MemberUSERNAME
        {
            get { return MemberUsername; }
            set { MemberUsername = value; }
        }

        public string MemberEMAIL
        {
            get { return MemberEmail; }
            set { MemberEmail = value; }
        }

        public string MemberPASSWORD
        {
            get { return MemberPassword; }
            set { MemberPassword = value; }
        }

        public string MemberPHONENUMBER
        {
            get { return MemberPhoneNumber; }
            set { MemberPhoneNumber = value; }
        }

        public string MemberADDRESS
        {
            get { return MemberAddress; }
            set { MemberAddress = value; }
        }

        public string MemberSPECIFICADDRESS
        {
            get { return MemberSpecificAddress; }
            set { MemberSpecificAddress = value; }
        }
    }
}

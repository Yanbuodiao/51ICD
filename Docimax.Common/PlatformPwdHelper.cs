using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Common
{
    public class PlatformPwdHelper
    {
        static List<string> upperCharecters = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z".Split(',').ToList();
        static List<string> lowerCharecters = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z".Split(',').ToList();
        static List<string> digtals = "0,1,2,3,4,5,6,7,8,9".Split(',').ToList();
        static List<string> symbols = "@,#,$,%,^,&,*,(,),-,_,+,=,-".Split(',').ToList();
        public static string CreatRadomPwd()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            var pwdLength = random.Next(6, 21);
            var upperLength = random.Next(1, pwdLength - 2);
            var lowerLength = random.Next(1, pwdLength - 1 - upperLength);
            var digtalLegth = random.Next(1, pwdLength - upperLength - lowerLength);
            var symbalLenth = pwdLength - upperLength - lowerLength - digtalLegth;
            var guid = Guid.NewGuid();

            var bb = upperCharecters.Select(e => new { ID = Guid.NewGuid(), e }).OrderBy(e => e.ID).ToList();
            var aa = upperCharecters.OrderBy(e => guid.ToString()).Take(upperLength);

            var result = string.Join("",
                symbols.Select(e => new { ID = Guid.NewGuid(), Str = e }).OrderBy(e => e.ID).Take(symbalLenth)
                 .Union(upperCharecters.Select(e => new { ID = Guid.NewGuid(), Str = e }).OrderBy(e => e.ID).Take(upperLength))
                 .Union(lowerCharecters.Select(e => new { ID = Guid.NewGuid(), Str = e }).OrderBy(e => e.ID).Take(lowerLength))
                 .Union(digtals.Select(e => new { ID = Guid.NewGuid(), Str = e }).OrderBy(e => e.ID).Take(digtalLegth))
                 .OrderBy(e => e.ID).Select(e => e.Str).ToArray());
            return result;
        }
    }
}

using System.Linq;
using NewsProject005.Models;
using NewsProject005.ViewModel;

namespace NewsProject005.Auth
{
    public class UserService
    {
        NewsDBEntities1 db = new NewsDBEntities1();

        public UsersModel UyeOturumAc(string kadi, string parola)
        {
            UsersModel login = db.tblUsers.Where(s => s.userName == kadi && s.password == parola).Select(x => new UsersModel()
            {
                Id = x.Id,
                images = x.images,
                nameSurname = x.nameSurname,
                mail = x.mail,
                userName = x.userName,
                password = x.password,
                authorityId = x.authorityId,
                facebook = x.facebook,
                instagram = x.instagram,
                twitter = x.twitter,
                linkedin = x.linkedin,
                editingDate = x.editingDate,
                createdDate = x.createdDate
            }).SingleOrDefault();
            return login;

        }
    }
}
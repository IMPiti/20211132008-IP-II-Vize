using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewsProject005.Models;
using NewsProject005.ViewModel;


namespace NewsProject005.Controllers
{
    public class ServiceController : ApiController
    {
        NewsDBEntities1 db = new NewsDBEntities1();
        ResultModel result = new ResultModel();

        #region News


        [HttpGet]
        [Route("api/newslist")]
        public List<NewsModel> NewsList()
        {
            List<NewsModel> newslist = db.tblNews.Select(x => new NewsModel()
            {
                Id = x.Id,
                createdDate = x.createdDate,
                editingDate = x.editingDate,
                newsConfirmation = x.newsConfirmation,
                newsCuff = x.newsCuff,
                newsDetail = x.newsDetail,
                newsSmallTitle = x.newsSmallTitle,
                newsStatus = x.newsStatus,
                newsSummary = x.newsSummary,
                newsTitle = x.newsTitle,
                newsTypeId = x.newsTypeId,
                newsUserAddId = x.newsUserAddId,
                newsViewing = x.newsViewing
            }).ToList();

            return newslist;
        }

        [HttpGet]
        [Route("api/newslist/{Id}")]
        public NewsModel NewsListById(int Id)
        {
            NewsModel kayit = db.tblNews.Where(s => s.Id == Id).Select(x=> new NewsModel()
            {
                Id = x.Id,
                createdDate = x.createdDate,
                editingDate = x.editingDate,
                newsConfirmation = x.newsConfirmation,
                newsCuff = x.newsCuff,
                newsDetail = x.newsDetail,
                newsSmallTitle = x.newsSmallTitle,
                newsStatus = x.newsStatus,
                newsSummary = x.newsSummary,
                newsTitle = x.newsTitle,
                newsTypeId = x.newsTypeId,
                newsUserAddId = x.newsUserAddId,
                newsViewing = x.newsViewing
                
            }).FirstOrDefault();

            return kayit;
        }


        [HttpPost]
        [Route("api/haberekle")]
        public ResultModel AddNews(NewsModel model)
        {
            if (db.tblNews.Count(s => s.newsTitle == model.newsTitle) > 0)
            {
                result.process = false;
                result.message = "Bu başlık önceden girilmiş";
                return result;
            }

            tblNews yeni = new tblNews();

            yeni.Id = model.Id;
            yeni.createdDate = model.createdDate;
            yeni.editingDate = model.editingDate;
            yeni.newsConfirmation = model.newsConfirmation;
            yeni.newsCuff = model.newsCuff;
            yeni.newsDetail = model.newsDetail;
            yeni.newsSmallTitle = model.newsSmallTitle;
            yeni.newsStatus = model.newsStatus;
            yeni.newsSummary = model.newsSummary;
            yeni.newsTitle = model.newsTitle;
            yeni.newsTypeId = model.newsTypeId;
            yeni.newsUserAddId = model.newsUserAddId;
            yeni.newsViewing = model.newsViewing;

            db.tblNews.Add(yeni);
            db.SaveChanges();

            // Ara tabloya kategori ilişkilendirmesini ekleme
            tblNewsCategoryMap ncm = new tblNewsCategoryMap
            {
                haber_id = yeni.Id,
                kategori_id = model.categoryId
            };

            db.tblNewsCategoryMap.Add(ncm);
            db.SaveChanges();

            result.process = true;
            result.message = "Haber eklendi.";
            return result;
        }


        [HttpPut]
        [Route("api/haberduzenle")]
        public ResultModel HaberDuzenle(NewsModel model)
        {
            tblNews kayit = db.tblNews.Where(s => s.Id == model.Id).FirstOrDefault();

            if (kayit == null)
            {
                result.process = false;
                result.message = "Böyle bir kayıt bulunamadı";
                return result;
            }

            kayit.Id = model.Id;
            kayit.createdDate = model.createdDate;
            kayit.editingDate = model.editingDate;
            kayit.newsConfirmation = model.newsConfirmation;
            kayit.newsCuff = model.newsCuff;
            kayit.newsDetail = model.newsDetail;
            kayit.newsSmallTitle = model.newsSmallTitle;
            kayit.newsStatus = model.newsStatus;
            kayit.newsSummary = model.newsSummary;
            kayit.newsTitle = model.newsTitle;
            kayit.newsTypeId = model.newsTypeId;
            kayit.newsUserAddId = model.newsUserAddId;
            kayit.newsViewing = model.newsViewing;
            db.SaveChanges();

            result.process = true;
            result.message = "Haber Düzenlendi.";
            return result;
        }

        [HttpDelete]
        [Route("api/habersil/{Id}")]
        public ResultModel HaberSil(int Id)
        {
            tblNews kayit = db.tblNews.Where(s => s.Id == Id).SingleOrDefault();

            if (kayit == null)
            {
                result.process = false;
                result.message = "Böyle bir kayıt bulunamadı";
                return result;
            }

            if (db.tblNewsCategoryMap.Count(s => s.haber_id == Id) > 0)
            {
                result.process = false;
                result.message = "Habere kayıtlı yazar bulunduğu için silinemez.";
                return result;
            }
            db.tblNews.Remove(kayit);
            db.SaveChanges();

            result.process = true;
            result.message = $"{kayit.newsTitle} başlıklı haber silindi";
            return result;
        }

        [HttpGet]
        [Route("api/habersoneklenenler/{s}")]
        public List<NewsModel> MakaleListeSonEklenenler(int s)
        {
            List<NewsModel> liste = db.tblNews.OrderByDescending(o => o.Id).Take(
           s).Select(x => new NewsModel()
           {
               Id = x.Id,
               createdDate = x.createdDate,
               editingDate = x.editingDate,
               newsConfirmation = x.newsConfirmation,
               newsCuff = x.newsCuff,
               newsDetail = x.newsDetail,
               newsSmallTitle = x.newsSmallTitle,
               newsStatus = x.newsStatus,
               newsSummary = x.newsSummary,
               newsTitle = x.newsTitle,
               newsTypeId = x.newsTypeId,
               //newsUserAddId = x.newsUserAddId,
               newsViewing = x.newsViewing,
               newsUserAddId = x.tblUsers.Id
           }).ToList();
            return liste;
        }
        #endregion

        #region Kullanıcı

        [HttpGet]
        [Route("api/kullanicilistele")]
        public List<UsersModel> KullaniciListe()
        {
            List<UsersModel> liste = db.tblUsers.Select(x => new UsersModel()
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

            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/kullanicibyid/{Id}")]
        public UsersModel KullaniciById(int Id)
        {
            UsersModel kayit = db.tblUsers.Where(s => s.Id == Id).Select(x => new UsersModel()
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
            return kayit;
        }


        [HttpPost]
        [Route("api/kullaniciekle")]
        public ResultModel KullaniciEkle(UsersModel model)
        {
            if (db.tblUsers.Count(s => s.userName == model.userName) > 0)
            {
                result.process = false;
                result.message = "Girilen kullanıcı adı mevcut!";
                return result;

            }

            tblUsers yeni = new tblUsers();
            yeni.Id = model.Id;
            yeni.images = model.images;
            yeni.nameSurname = model.nameSurname;
            yeni.mail = model.mail;
            yeni.userName = model.userName;
            yeni.password = model.password;
            yeni.authorityId = model.authorityId;
            yeni.facebook = model.facebook;
            yeni.instagram = model.instagram;
            yeni.twitter = model.twitter;
            yeni.linkedin = model.linkedin;
            yeni.editingDate = model.editingDate;
            yeni.createdDate = model.createdDate;

            db.tblUsers.Add(yeni);
            db.SaveChanges();

            result.process = true;
            result.message = $"{yeni.nameSurname} adlı kullanıcı eklendi.";
            return result;
        }

        [HttpPost]
        [Route("api/usrfotografguncelle")]
        public ResultModel UsrPhotUpdate(UserImageModel model)
        {
            tblUsers user = db.tblUsers.Where(s => s.Id == model.Id).SingleOrDefault(
           );
            if (user == null)
            {
                result.process = false;
                result.message = "Kayıt Bulunamadı!";
                return result;
            }
            if (user.images != "profil.jpg")
            {
                string yol = System.Web.Hosting.HostingEnvironment.MapPath("~/Dosyalar/"
               + user.images);
                if (File.Exists(yol))
                {
                    File.Delete(yol);
                }
            }
            string data = model.fotoData;
            string base64 = data.Substring(data.IndexOf(',') + 1);
            base64 = base64.Trim('\0');
            byte[] imgbytes = Convert.FromBase64String(base64);
            string dosyaAdi = user.Id + model.fotoUzanti.Replace("image/", ".");
            using (var ms = new MemoryStream(imgbytes, 0, imgbytes.Length))
            {
                Image img = Image.FromStream(ms, true);
                img.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/Dosyalar/" + dosyaAdi));
            }
            user.images = dosyaAdi;
            db.SaveChanges();
            result.process = true;
            result.message = "Fotoğraf Güncellendi";
            return result;
        }


        [HttpDelete]
        [Route("api/kullanicisil/{kullaniciId}")]

        public ResultModel KullaniciSil(int Id)
        {
            tblUsers kayit = db.tblUsers.Where(s => s.Id == Id).SingleOrDefault();
             
            if (kayit == null)
            {
                result.process = false;
                result.message = "Kayit Bulunamadı";
                return result;
            }
            if (db.tblNews.Count(s => s.newsUserAddId == Id) > 0)
            {
                result.process = false;
                result.message = "Üzerinde Haber Kaydı olan üye silinemez";
                return result;
            }
            if (db.tblNews.Count(s => s.newsUserAddId == Id) > 0)
            {
                result.process = false;
                result.message = "Üzerinde Yorum Kaydı olan üye silinemez";
                return result;
            }
            db.tblUsers.Remove(kayit);
            db.SaveChanges();
            result.process = true;
            result.message = "Üye Silindi";
            return result;

        }

        #endregion

        #region Kategori

        [HttpGet]
        [Route("api/categorylist")]
        public List<CategorysModel> CategoriesList()
        {
            List<CategorysModel> catlist = db.tblCategorys.Select(x => new CategorysModel()
            {
                id = x.id,
                categoryName = x.categoryName,
                lockCategory = x.lockCategory,
                status = x.status,
                createdDate = x.createdDate,
                editingDate = x.editingDate
            }).ToList();

            return catlist;
        }

        [HttpGet]
        [Route("api/categorieslist/{id}")]
        public CategorysModel CategoriesListById(int id)
        {
            CategorysModel catlist = db.tblCategorys.Where(s => s.id == id).Select(x => new CategorysModel()
            {
                id = x.id,
                categoryName = x.categoryName,
                lockCategory = x.lockCategory,
                status = x.status,
                createdDate = x.createdDate,
                editingDate = x.editingDate
            }).FirstOrDefault();

            return catlist;
        }

       
        [HttpPost]
        [Route("api/addcategory")]
        public ResultModel AddCategory(CategorysModel model)
        {

            if (db.tblCategorys.Count(s => s.categoryName == model.categoryName) > 0)
            {
                result.process = false;
                result.message = "Bu Kategori önceden girilmiş";
                return result;
            }

            tblCategorys yeni = new tblCategorys();

            yeni.id = model.id;
            yeni.categoryName = model.categoryName;
            yeni.lockCategory = model.lockCategory;
            yeni.status = model.status;
            yeni.createdDate = model.createdDate;
            yeni.editingDate = model.editingDate;

            db.tblCategorys.Add(yeni);
            db.SaveChanges();

            result.process = true;
            result.message = "Kategori eklendi.";
            return result;
        }

        [HttpPut]
        [Route("api/kategoriduzenle")]
        public ResultModel KategoriDuzenle(CategorysModel model)
        {
            tblCategorys kayit = db.tblCategorys.Where(s => s.id == model.id).FirstOrDefault();

            if (kayit == null)
            {
                result.process = false;
                result.message = "Böyle bir kayıt bulunamadı";
                return result;
            }

            kayit.id = model.id;
            kayit.categoryName = model.categoryName;
            kayit.lockCategory = model.lockCategory;
            kayit.status = model.status;
            kayit.createdDate = model.createdDate;
            kayit.editingDate = model.editingDate;
            db.SaveChanges();

            result.process = true;
            result.message = "Kategori Düzenlendi.";
            return result;
        }

        [HttpDelete]
        [Route("api/kategorisil/{id}")]
        public ResultModel KategoriSil(int id)
        {
            tblCategorys kayit = db.tblCategorys.Where(s => s.id == id).SingleOrDefault();

            if (kayit == null)
            {
                result.process = false;
                result.message = "Böyle bir kayıt bulunamadı";
                return result;
            }

            if (db.tblNewsCategoryMap.Count(s => s.haber_id == id) > 0)
            {
                result.process = false;
                result.message = "Kategoriye kayıtlı haber bulunduğu için silinemez.";
                return result;
            }
            db.tblCategorys.Remove(kayit);
            db.SaveChanges();

            result.process = true;
            result.message = $"{kayit.categoryName} başlıklı haber silindi";
            return result;
        }


        [HttpGet]
        [Route("api/newslist/category/{categoryId}")]
        public List<NewsModel> NewsListByCategory(int categoryId)
        {
            var newsList = db.tblNewsCategoryMap.Where(ncm => ncm.kategori_id == categoryId).Select(x => new NewsModel()
            {
                Id = x.tblNews.Id,
                createdDate = x.tblNews.createdDate,
                editingDate = x.tblNews.editingDate,
                newsConfirmation = x.tblNews.newsConfirmation,
                newsCuff = x.tblNews.newsCuff,
                newsDetail = x.tblNews.newsDetail,
                newsSmallTitle = x.tblNews.newsSmallTitle,
                newsStatus = x.tblNews.newsStatus,
                newsSummary = x.tblNews.newsSummary,
                newsTitle = x.tblNews.newsTitle,
                newsTypeId = x.tblNews.newsTypeId,
                newsUserAddId = x.tblNews.newsUserAddId,
                newsViewing = x.tblNews.newsViewing,
                categoryId = x.kategori_id // categoryId özelliğini burada kullanabilirsiniz
            }).ToList();

            return newsList;
        }


        #endregion

        #region Kayit

        [HttpGet]
        [Route("api/kullanicihaberliste/{userId}")]
        public List<NewsModel> KullaniciHaberListe(int userId)
        {
            List<NewsModel> liste = db.tblNews.Where(s => s.newsUserAddId == userId).Select(x => new NewsModel()
            {
                Id = x.Id,
                createdDate = x.createdDate,
                editingDate = x.editingDate,
                newsConfirmation = x.newsConfirmation,
                newsCuff = x.newsCuff,
                newsDetail = x.newsDetail,
                newsSmallTitle = x.newsSmallTitle,
                newsStatus = x.newsStatus,
                newsSummary = x.newsSummary,
                newsTitle = x.newsTitle,
                newsTypeId = x.newsTypeId,
                newsUserAddId = x.newsUserAddId,
                newsViewing = x.newsViewing
            }).ToList();

            return liste;
        }



        [HttpGet]
        [Route("api/kategorihaberlistele/{kategori_id}")]
        public List<NewsCategoryMapModel> KategoriHaberListele(int kategori_id)
        {
            List<NewsCategoryMapModel> liste = db.tblNewsCategoryMap.Where(s => s.Id == kategori_id
            ).Select(x => new NewsCategoryMapModel()
            {
                Id = x.Id,
                haber_id = x.haber_id,
                kategori_id = x.kategori_id

            }).ToList();


            foreach (var kayit in liste)
            {

               // kayit.kullaniciBilgi = NewsListById(kayit.kategori_id);
                kayit.haberBilgi = NewsListById(kayit.haber_id);
            }
            return liste;
        }

        [HttpPost]
        [Route("api/kayitekle")]
        public ResultModel KayitEkle(tblNewsCategoryMap model)
        {
            if (db.tblNewsCategoryMap.Count(s => s.haber_id == model.haber_id && s.kategori_id == model.kategori_id) > 0)
            {
                result.process = false;
                result.message = "İlgili kategoriye dair bir kayit bulunamadı";
                return result;
            }


            tblNewsCategoryMap yeni = new tblNewsCategoryMap();
            yeni.Id = model.Id;
            yeni.kategori_id = model.kategori_id;
            yeni.haber_id = model.haber_id;
            db.tblNewsCategoryMap.Add(yeni);
            db.SaveChanges();

            result.process = true;
            result.message = "Haber kategoriye eklendi.";
            return result;
        }

        [HttpDelete]
        [Route("api/kayitsil/{kayitId}")]
        public ResultModel KayitSil(int Id)
        {
            tblNewsCategoryMap kayit = db.tblNewsCategoryMap.Where(s => s.Id == Id).SingleOrDefault();

            if (kayit == null)
            {
                result.process = false;
                result.message = "Kayıt Bulunamadı";
                return result;
            }
            db.tblNewsCategoryMap.Remove(kayit);
            db.SaveChanges();

            result.process = true;
            result.message = "Haber Kaydi Silindi";
            return result;
        }




        #endregion

        #region yetki

        [HttpPost]
        [Route("api/yetkiekle")]
        public ResultModel YetkiEkle(AuthorityModel model)
        {
            if (db.tblAuthority.Count(s => s.Id == model.Id) > 0)
            {
                result.process = false;
                result.message = "Girilen yetki mevcut!";
                return result;

            }

            tblAuthority yeni = new tblAuthority();
            yeni.Id = model.Id;
            yeni.authorityName = model.authorityName;
       


            db.tblAuthority.Add(yeni);
            db.SaveChanges();

            result.process = true;
            result.message = $"{model. authorityName} adlı yetki eklendi.";
            return result;
        }

        [HttpDelete]
        [Route("api/yetkisil/{kayitId}")]
        public ResultModel YetkiSil(int Id)
        {
            tblAuthority yetki = db.tblAuthority.Where(s => s.Id == Id).SingleOrDefault();

            if (yetki == null)
            {
                result.process = false;
                result.message = "Kayıt Bulunamadı";
                return result;
            }
            db.tblAuthority.Remove(yetki);
            db.SaveChanges();

            result.process = true;
            result.message = "Haber Kaydi Silindi";
            return result;
        }
        #endregion
        
        #region Yorum
        [HttpGet]
        [Route("api/yorumliste")]
        public List<YorumModel> YorumListe()
        {
            List<YorumModel> liste = db.tblComments.Select(x => new YorumModel()
            {
                YorumId = x.YorumId,
                YorumIcerik = x.YorumIcerik,
                HaberId = x.HaberId,
                UyeId = x.UyeId,
                ////Tarih = x.Tarih,
                KullaniciAdi = x.tblUsers.nameSurname,
                MakaleBaslik = x.tblNews.newsTitle,

            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/yorumlistebyuyeid/{uyeId}")]
        public List<YorumModel> YorumListeByUyeId(int uyeId)
        {
            List<YorumModel> liste = db.tblComments.Where(s => s.UyeId == uyeId).Select(x => new YorumModel()
            {
                YorumId = x.YorumId,
                YorumIcerik = x.YorumIcerik,
                HaberId = x.HaberId,
                UyeId = x.UyeId,
                //Tarih = x.Tarih,
                KullaniciAdi = x.tblUsers.nameSurname,
                MakaleBaslik = x.tblNews.newsTitle,

            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/yorumlistebymakaleid/{makaleId}")]
        public List<YorumModel> YorumListeBymakaleId(int makaleId)
        {
            List<YorumModel> liste = db.tblComments.Where(s => s.HaberId == makaleId).Select(x => new YorumModel()
            {
                YorumId = x.YorumId,
                YorumIcerik = x.YorumIcerik,
                HaberId = x.HaberId,
                UyeId = x.UyeId,
                //Tarih = x.Tarih,
                KullaniciAdi = x.tblUsers.nameSurname,
                MakaleBaslik = x.tblNews.newsTitle,

            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/yorumlistesoneklenenler/{s}")]
        public List<YorumModel> YorumListeSonEklenenler(int s)
        {
            List<YorumModel> liste = db.tblComments.OrderByDescending(o => o.HaberId).Take(s).Select(x => new YorumModel()
            {
                YorumId = x.YorumId,
                YorumIcerik = x.YorumIcerik,
                HaberId = x.HaberId,
                UyeId = x.UyeId,
                //Tarih = x.Tarih,
                KullaniciAdi = x.tblUsers.nameSurname,
                MakaleBaslik = x.tblNews.newsTitle,

            }).ToList();
            return liste;
        }


        [HttpGet]
        [Route("api/yorumbyid/{yorumId}")]

        public YorumModel YorumById(int yorumId)
        {
            YorumModel kayit = db.tblComments.Where(s => s.YorumId == yorumId).Select(x => new YorumModel()
            {
                YorumId = x.YorumId,
                YorumIcerik = x.YorumIcerik,
                HaberId = x.HaberId,
                UyeId = x.UyeId,
                //Tarih = x.Tarih,
                KullaniciAdi = x.tblUsers.nameSurname,
                MakaleBaslik = x.tblNews.newsTitle,
            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/yorumekle")]
        public ResultModel YorumEkle(YorumModel model)
        {
            if (db.tblComments.Count(s => s.UyeId == model.UyeId && s.HaberId == model.HaberId && s.YorumIcerik == model.YorumIcerik) > 0)
            {
                result.process = false;
                result.message = "Aynı Kişi, Aynı Makaleye Aynı Yorumu Yapamaz!";
                return result;
            }

            tblComments yeni = new tblComments();
            yeni.YorumId = model.YorumId;
            yeni.YorumIcerik = model.YorumIcerik;
            yeni.HaberId = model.HaberId;
            yeni.UyeId = model.UyeId;
            yeni.Tarih = model.Tarih;
            db.tblComments.Add(yeni);
            db.SaveChanges();

            result.process = true;
            result.message = "Yorum Eklendi";

            return result;
        }
        [HttpPut]
        [Route("api/yorumduzenle")]
        public ResultModel YorumDuzenle(YorumModel model)
        {

            tblComments kayit = db.tblComments.Where(s => s.YorumId == model.YorumId).SingleOrDefault();

            if (kayit == null)
            {
                result.process = false;
                result.message = "Kayıt Bulunamadı!";
                return result;
            }

            kayit.YorumId = model.YorumId;
            kayit.YorumIcerik = model.YorumIcerik;
            kayit.HaberId = model.HaberId;
            kayit.UyeId = model.UyeId;
            kayit.Tarih = model.Tarih;

            db.SaveChanges();

            result.process = true;
            result.message = "Yorum Düzenlendi";

            return result;
        }

        [HttpDelete]
        [Route("api/yorumsil/{yorumId}")]
        public ResultModel YorumSil(int yorumId)
        {
            tblComments kayit = db.tblComments.Where(s => s.YorumId == yorumId).SingleOrDefault();

            if (kayit == null)
            {
                result.process = false;
                result.message = "Kayıt Bulunamadı!";
                return result;
            }

            db.tblComments.Remove(kayit);
            db.SaveChanges();

            result.process = true;
            result.message = "Yorum Silindi";

            return result;
        }


        #endregion
        
    }
}

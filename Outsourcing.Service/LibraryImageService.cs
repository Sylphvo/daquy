using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Repository;
using Outsourcing.Service.Properties;
using Outsourcing.Data.Utils;
using System.Web;

namespace Outsourcing.Service
{
    public interface ILibraryImageService
    {
        void Save(HttpPostedFileBase[] files, string pathSv, int type,int? typeImge = null, string iframe = null, int? typeVideo = null, int? productId = null);
        void Update(HttpPostedFileBase[] files, string pathSv, int type, int id, int? typeImge = null, string iframe = null, int? typeVideo = null, int? productId = null);
        IEnumerable<LibraryImage> GetByType(int type);
        IEnumerable<LibraryImage> GetCourse(int type,int productId);
        IEnumerable<LibraryImage> Getimage();
        LibraryImage GetLibraryImageById(int imageId);
        void CreateLibraryImage(LibraryImage image);
        void EditLibraryImage(LibraryImage imageToEdit);
        void DeleteLibraryImage(int imageId);
        void SaveLibraryImage();
        LibraryImage GetLibraryImageByUrlName(string urlName);
    }
    public class LibraryImageService : ILibraryImageService
    {
        #region Field
        private readonly ILibraryImageRepository imageRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public LibraryImageService(ILibraryImageRepository imageRepository, IUnitOfWork unitOfWork)
        {
            this.imageRepository = imageRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        public void Update(HttpPostedFileBase[] files, string pathSv, int type, int id, int? typeImge = null, string iframe = null, int? typeVideo = null, int? productId = null)
        {
            var temp = imageRepository.GetById(id);
            switch (type)
            {
                case (int)Enums.LibraryImageType.Image:
                    temp.Temp1 = typeImge;
                    if (files != null)
                    {
                        var item = files[0];
                        temp.Url = Utils.SaveFile(pathSv, item, type);
                    }
                    imageRepository.Update(temp);

                    SaveLibraryImage();
                    break;
                case (int)Enums.LibraryImageType.Video:

                    if (typeVideo != null)
                    {

                        temp.Type = type;
                        temp.Temp1 = typeVideo.Value;
                        switch (typeVideo)
                        {
                            case (int)Enums.VideoEnum.Youtube:
                                temp.Temp2 = iframe;
                                imageRepository.Update(temp);
                                SaveLibraryImage();
                                break;
                            case (int)Enums.VideoEnum.Server:
                                if (files != null)
                                {
                                    var item = files[0];
                                    temp.Url = Utils.SaveFile(pathSv, item, type);
                                    imageRepository.Add(temp);
                                    SaveLibraryImage();
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    break;
                case (int)Enums.LibraryImageType.Course:

                    if (typeVideo != null)
                    {

                        temp.Type = type;
                        temp.Temp1 = typeVideo.Value;
                        temp.Temp3 = productId.Value;
                        switch (typeVideo)
                        {
                            case (int)Enums.VideoEnum.Youtube:
                                temp.Temp2 = iframe;
                                imageRepository.Update(temp);
                                SaveLibraryImage();
                                break;
                            case (int)Enums.VideoEnum.Server:
                                if (files != null)
                                {
                                    var item = files[0];
                                    temp.Url = Utils.SaveFile(pathSv, item, type);
                                    imageRepository.Add(temp);
                                    SaveLibraryImage();
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    break;
                default:
                    break;
            }
        }
        public void Save(HttpPostedFileBase[] files, string pathSv, int type,int? typeImge = null, string iframe = null, int? typeVideo = null,int? productId = null)
        {
            switch (type)
            {
                case (int)Enums.LibraryImageType.Image:
                    if (files != null)
                    {
                        foreach (var item in files)
                        {
                            var temp = new LibraryImage();
                            temp.Type = type;
                            temp.Temp1 = typeImge;
                            temp.Url = Utils.SaveFile(pathSv, item, type);
                            imageRepository.Add(temp);
                        }
                        SaveLibraryImage();
                    }
                    break;
                case (int)Enums.LibraryImageType.Video:

                    if (typeVideo != null)
                    {
                        var temp = new LibraryImage();
                        temp.Type = type;
                        temp.Temp1 = typeVideo.Value;
                        switch (typeVideo)
                        {
                            case (int)Enums.VideoEnum.Youtube:
                                temp.Temp2 = iframe;
                                imageRepository.Add(temp);
                                SaveLibraryImage();
                                break;
                            case (int)Enums.VideoEnum.Server:
                                if (files != null)
                                {
                                    var item = files[0];
                                    temp.Url = Utils.SaveFile(pathSv, item, type);
                                    imageRepository.Add(temp);
                                    SaveLibraryImage();
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    break;
                case (int)Enums.LibraryImageType.Course:

                    if (typeVideo != null)
                    {
                        var temp = new LibraryImage();
                        temp.Type = type;
                        temp.Temp1 = typeVideo.Value;
                        temp.Temp3 = productId.Value;
                        switch (typeVideo)
                        {
                            case (int)Enums.VideoEnum.Youtube:
                                temp.Temp2 = iframe;
                                imageRepository.Add(temp);
                                SaveLibraryImage();
                                break;
                            case (int)Enums.VideoEnum.Server:
                                if (files != null)
                                {
                                    var item = files[0];
                                    temp.Url = Utils.SaveFile(pathSv, item, type);
                                    imageRepository.Add(temp);
                                    SaveLibraryImage();
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    break;
                default:
                    break;
            }

        }
        public IEnumerable<LibraryImage> GetByType(int type)
        {
            var images = imageRepository.GetMany(p =>
                            p.Type == type
                           //&& p.Status == (int)Enums.LibraryImageStatus.Active
            ).OrderByDescending(b => b.CreateDate);
            return images;
        }
        public IEnumerable<LibraryImage> GetCourse(int type, int productId)
        {
            var images = imageRepository.GetMany(p =>
                           p.Type == type
                           && p.Temp3 == productId
                          && p.Status == (int)Enums.LibraryImageStatus.Active
           ).OrderByDescending(b => b.CreateDate);
            return images;
        }

        #region Implementation for ILibraryImageService
        public IEnumerable<LibraryImage> Getimage()
        {
            var images = imageRepository.GetAll().OrderBy(b => b.CreateDate);
            return images;
        }
        public IEnumerable<LibraryImage> Get3imagePosition()
        {
            var images = imageRepository.GetAll().OrderBy(b => b.CreateDate).Take(3);
            return images;
        }
        public IEnumerable<LibraryImage> GetHomePageimage()
        {
            var images = imageRepository.
               GetAlls().
                OrderByDescending(b => b.CreateDate);
            return images;
        }




        public LibraryImage GetLibraryImageById(int imageId)
        {
            var image = imageRepository.GetById(imageId);
            return image;
        }

        public void CreateLibraryImage(LibraryImage image)
        {
            imageRepository.Add(image);
            SaveLibraryImage();
        }

        public void EditLibraryImage(LibraryImage imageToEdit)
        {

            imageRepository.Update(imageToEdit);
            SaveLibraryImage();
        }

        public void DeleteLibraryImage(int imageId)
        {
            //Get image by id.
            var image = imageRepository.GetById(imageId);
            if (image != null)
            {
                image.Status = (int)Enums.LibraryImageStatus.Deactive;
                imageRepository.Update(image);
                SaveLibraryImage();
            }
        }

        public void SaveLibraryImage()
        {
            unitOfWork.Commit();
        }

        public LibraryImage GetLibraryImageByUrlName(string urlName)
        {
            var image = imageRepository.Get(b => b.Slug == urlName);
            return image;
        }
        #endregion





    }
}

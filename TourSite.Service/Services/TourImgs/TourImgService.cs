using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Store.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TourSite.Core;
using TourSite.Core.DTOs.CategoryTour;

using TourSite.Core.DTOs.TourImg;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;
using TourSite.Core.Specification.CatgeoryTour;

using TourSite.Core.Specification.TourImgs;

namespace TourSite.Service.Services.TourImgs
{
    public class TourImgService : ITourImgService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public IWebHostEnvironment env { get; }


        public TourImgService(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment _env)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            env = _env;
        }



        public async Task<PageinationResponse<TourImgDto>> GetAllTourImgsAsync(TourImgSpeciParams specParams)
        {
            var spec = new TourImgSpecification(specParams);
            var allData = await unitOfWork.Repository<TourImg>().GetAllSpecAsync(spec);

            if (allData is null) return null;

            // ✅ استخدم AutoMapper مباشرة مع الـ language
            var data = mapper.Map<IEnumerable<TourImgDto>>(allData);

            // Count بدون ترجمات
            var CountSpec = new TourImgWithCountSpecifications(specParams);
            var Count = await unitOfWork.Repository<TourImg>().GetCountAsync(CountSpec);

            return new PageinationResponse<TourImgDto>(specParams.pageIndex, specParams.pageSize, Count, data);
        }

        public async Task<PageinationResponse<TourImgDto>> GetAllTourImgsAdminAsync(TourImgSpeciParams specParams)
        {

            var spec = new TourImgSpecificationForAdmin(specParams);
            var allData = await unitOfWork.Repository<TourImg>().GetAllSpecAsync(spec);

            if (allData is null) return null;

            // ✅ استخدم AutoMapper مباشرة مع الـ language
            var data = mapper.Map<IEnumerable<TourImgDto>>(allData);

            // Count بدون ترجمات
            var CountSpec = new TourImgWithCountSpecifications(specParams);
            var Count = await unitOfWork.Repository<TourImg>().GetCountAsync(CountSpec);

            return new PageinationResponse<TourImgDto>(specParams.pageIndex, specParams.pageSize, Count, data);
        }

        public async Task<TourImgDto> GetTourImgByIdAsync(int id)
        {
            var spec = new TourImgSpecification(id);
            var CategoryTour = await unitOfWork.Repository<TourImg>().GetByIdSpecAsync(spec);

            return mapper.Map<TourImgDto>(CategoryTour);
        }

        public async Task AddImgTourAsync(TourImgCreateDto ImgTourToutDto)
        {

            // 🖼️ حفظ الصورة في wwwroot/images/categoryTours
            string imagePath = string.Empty;
            if (ImgTourToutDto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/tourImgs");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + Path.GetExtension(ImgTourToutDto.ImageFile.FileName);
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await ImgTourToutDto.ImageFile.CopyToAsync(stream);
                }

                imagePath = $"images/tourImgs/{fileName}";
            }


            // 🧩 إنشاء الكيان
            var tourImg = new TourImg
            {
                ImageCarouselUrl = imagePath,
                IsActive = ImgTourToutDto.IsActive,
                FK_TourId = ImgTourToutDto.FK_TourId,
                Title= ImgTourToutDto.Title,
                ReferenceName= ImgTourToutDto.ReferenceName

            };

            await unitOfWork.Repository<TourImg>().AddAsync(tourImg);
            await unitOfWork.CompleteAsync();
        }


        public async Task<bool> UpdatImgTour(TourImgCreateDto dto, int id)
        {
            // استخدم الـ spec الجديد
            var spec = new TourImgForUpdateSpec(id);
            var tourimg = unitOfWork.Repository<TourImg>().GetByIdSpecTEntityAsync(spec);

            if (tourimg == null)
            {
                return false;
            }

            // ✅ تحديث الصورة (لو تم رفع واحدة جديدة)
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/tourImgs");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + Path.GetExtension(dto.ImageFile.FileName);
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(stream);
                }

                tourimg.ImageCarouselUrl = $"images/tourImgs/{fileName}";
            }

            // ✅ تحديث الحالة
            tourimg.IsActive = dto.IsActive;
            tourimg.FK_TourId = dto.FK_TourId;
            tourimg.Title = dto.Title;
            tourimg.ReferenceName = dto.ReferenceName;


            // ✅ تحديث الكيان
            unitOfWork.Repository<TourImg>().Update(tourimg);
            // ✅ حفظ التغييرات
            await unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteImgTour(int id)
        {
            var spec = new TourImgForUpdateSpec(id);

            var tourimg = unitOfWork.Repository<TourImg>().GetByIdSpecTEntityAsync(spec);
            if (tourimg == null)
            {
                return false;
            }

            unitOfWork.Repository<TourImg>().Delete(tourimg);

            await unitOfWork.CompleteAsync();

            return true;
        }

    }
}

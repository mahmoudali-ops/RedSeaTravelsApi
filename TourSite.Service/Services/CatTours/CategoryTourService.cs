using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TourSite.Core;
using TourSite.Core.DTOs.CategorToutCreateDto;
using TourSite.Core.DTOs.CategoryTour;
using TourSite.Core.DTOs.TourImg;
using TourSite.Core.DTOs.Tours;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;
using TourSite.Core.Specification.CatgeoryTour;
using TourSite.Core.Specification.Tours;
using TourSite.Repository.Data.Contexts;
using TourSite.Repository.Repositories;

namespace TourSite.Service.Services.CatTours
{
public class CategoryTourService : ICategoryTourService
{
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public IWebHostEnvironment env { get; }



        public CategoryTourService(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment _env)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            env = _env;
        }

        public async Task<PageinationResponse<CategorToutAllDto>> GetAllCatToursAsync(CatTourSpecParams specParams)
        {
         
            var spec = new CatTourSpecification(specParams);
            var allData = await unitOfWork.Repository<CategoryTour>().GetAllSpecAsync(spec);

            if (allData is null) return null;

            // ✅ استخدم AutoMapper مباشرة مع الـ language
            var data = mapper.Map<IEnumerable<CategorToutAllDto>>(allData);

            // Count بدون ترجمات
            var CountSpec = new CatTourWithCountSpecifications(specParams);
            var Count = await unitOfWork.Repository<CategoryTour>().GetCountAsync(CountSpec);

            return new PageinationResponse<CategorToutAllDto>(specParams.pageIndex, specParams.pageSize, Count, data);
        }

        public async Task<PageinationResponse<CategorToutAllDto>> GetAllCatToursAdminAsync(CatTourSpecParams cattourSpecParams)
        {
            

            var spec = new CatTourSpecificationForAdmin(cattourSpecParams);
            var allData = await unitOfWork.Repository<CategoryTour>().GetAllSpecAsync(spec);

            if (allData is null) return null;

            // ✅ استخدم AutoMapper مباشرة مع الـ language
            var data = mapper.Map<IEnumerable<CategorToutAllDto>>(allData);

            // Count بدون ترجمات
            var CountSpec = new CatTourWithCountSpecifications(cattourSpecParams);
            var Count = await unitOfWork.Repository<CategoryTour>().GetCountAsync(CountSpec);

            return new PageinationResponse<CategorToutAllDto>(cattourSpecParams.pageIndex, cattourSpecParams.pageSize, Count, data);
        }

        public async Task<CategorToutDto> GetCatTourByIdAsync(int id)
        {
            var spec = new CatTourSpecification(id);
            var CategoryTour = await unitOfWork.Repository<CategoryTour>().GetByIdSpecAsync(spec);

            return mapper.Map<CategorToutDto>(CategoryTour);
        }


        public async Task AddCatTourAsync(CategorToutCreateDto categorToutDto)
        {


            // 🖼️ حفظ الصورة في wwwroot/images/categoryTours
            string imagePath = string.Empty;
            if (categorToutDto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/categoryTours");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + Path.GetExtension(categorToutDto.ImageFile.FileName);
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await categorToutDto.ImageFile.CopyToAsync(stream);
                }

                imagePath = $"images/categoryTours/{fileName}";
            }

            // 🧩 إنشاء الكيان
            var CategoryTour = new CategoryTour
            {
                ImageCover = imagePath,
                IsActive = categorToutDto.IsActive,
                ReferenceName = categorToutDto.ReferenceName,
                Title = categorToutDto.Title,
                Description = categorToutDto.Description,
                MetaDescription = categorToutDto.MetaDescription,
                MetaKeyWords = categorToutDto.MetaKeyWords


            };

            await unitOfWork.Repository<CategoryTour>().AddAsync(CategoryTour);
            await unitOfWork.CompleteAsync();
        }

        public async Task<Boolean> UpdateCatTour(CategorToutCreateDto dto, int id)
        {

            // استخدم الـ spec الجديد
            var spec = new CategoryTourForUpdateSpec(id);
            var categoryTour = unitOfWork.Repository<CategoryTour>().GetByIdSpecTEntityAsync(spec);

            if (categoryTour == null)
            {
                return false;
            }




            // ✅ تحديث الصورة (لو تم رفع واحدة جديدة)
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/categoryTours");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + Path.GetExtension(dto.ImageFile.FileName);
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    dto.ImageFile.CopyTo(stream);
                }

                categoryTour.ImageCover = $"images/categoryTours/{fileName}";
            }

            // ✅ تحديث الحالة
            categoryTour.IsActive = dto.IsActive;
            categoryTour.ReferenceName = dto.ReferenceName;
            categoryTour.Title = dto.Title;
            categoryTour.Description = dto.Description;
            categoryTour.MetaDescription = dto.MetaDescription;
            categoryTour.MetaKeyWords = dto.MetaKeyWords;


            // ✅ تحديث الكيان
            unitOfWork.Repository<CategoryTour>().Update(categoryTour);
            // ✅ حفظ التغييرات
            await unitOfWork.CompleteAsync();
            return true;

        }

        public async Task<Boolean> DeleteCatTour(int id)
        {
            var spec = new CategoryTourForUpdateSpec(id);

            var categoryTour = unitOfWork.Repository<CategoryTour>().GetByIdSpecTEntityAsync(spec);
            if (categoryTour == null)
            {
                return false;
            }

            unitOfWork.Repository<CategoryTour>().Delete(categoryTour);

            await unitOfWork.CompleteAsync();

            return true;
        }

    }
}

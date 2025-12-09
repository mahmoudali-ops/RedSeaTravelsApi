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
using TourSite.Core.DTOs.CategorToutCreateDto;
using TourSite.Core.DTOs.CategoryTour;
using TourSite.Core.DTOs.Destnation;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;
using TourSite.Core.Specification.CatgeoryTour;
using TourSite.Core.Specification.Destnations;

namespace TourSite.Service.Services.Destnations
{
    public class DestnationService : IDestinationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public IWebHostEnvironment env { get; }


        public DestnationService(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment _env)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            env = _env;
        }




        public async Task<PageinationResponse<DestnationAllDto>> GetAllDestToursAsync(DestnationSpecParams specParams)
        {
           

            var spec = new DestnationSpecification(specParams);
            var allData = await unitOfWork.Repository<Destination>().GetAllSpecAsync(spec);

            if (allData is null) return null;

            // ✅ استخدم AutoMapper مباشرة مع الـ language
            var data = mapper.Map<IEnumerable<DestnationAllDto>>(allData);

            // Count بدون ترجمات
            var CountSpec = new DestnationWitCountSpecification(specParams);
            var Count = await unitOfWork.Repository<Destination>().GetCountAsync(CountSpec);

            return new PageinationResponse<DestnationAllDto>(specParams.pageIndex, specParams.pageSize, Count, data);
        }

        public async Task<PageinationResponse<DestnationAllDto>> GetAllDestToursAdminAsync(DestnationSpecParams DestnationSpecParams)
        {
            
            var spec = new DestnationSpecificationForAdmin(DestnationSpecParams);
            var allData = await unitOfWork.Repository<Destination>().GetAllSpecAsync(spec);

            if (allData is null) return null;

            // ✅ استخدم AutoMapper مباشرة مع الـ language
            var data = mapper.Map<IEnumerable<DestnationAllDto>>(allData);

            // Count بدون ترجمات
            var CountSpec = new DestnationWitCountSpecification(DestnationSpecParams);
            var Count = await unitOfWork.Repository<Destination>().GetCountAsync(CountSpec);

            return new PageinationResponse<DestnationAllDto>(DestnationSpecParams.pageIndex, DestnationSpecParams.pageSize, Count, data);
        }

        public async Task<DestnationDto> GetDestByIdAsync(int id)
        {
            var spec = new DestnationSpecification(id);
            var CategoryTour = await unitOfWork.Repository<Destination>().GetByIdSpecAsync(spec);

            return mapper.Map<DestnationDto>(CategoryTour);
        }

        public async Task AddDestnationAsync(DestnationToutCreateDto DestCreateDto)
        {
          
            // 🖼️ حفظ الصورة في wwwroot/images/categoryTours
            string imagePath = string.Empty;

            if (DestCreateDto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/destinations");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + Path.GetExtension(DestCreateDto.ImageFile.FileName);
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await DestCreateDto.ImageFile.CopyToAsync(stream);
                }

                imagePath = $"images/destinations/{fileName}";
            }

            // 🧩 إنشاء الكيان
            var destination = new Destination
            {
                ImageCover = imagePath,
                IsActive = DestCreateDto.IsActive,
                ReferenceName = DestCreateDto.ReferenceName,
                MetaDescription = DestCreateDto.MetaDescription,
                MetaKeyWords = DestCreateDto.MetaKeyWords,
                Title = DestCreateDto.Title,
                Description = DestCreateDto.Description
            };

            await unitOfWork.Repository<Destination>().AddAsync(destination);
            await unitOfWork.CompleteAsync();
        }


        public async Task<bool> UpdateDest(DestnationToutCreateDto dto, int id)
        {
            // استخدم الـ spec الجديد
            var spec = new DestnationForUpdateSpec(id);
            var destination = unitOfWork.Repository<Destination>().GetByIdSpecTEntityAsync(spec);

            if (destination == null)
            {
                return false;
            }

            // ✅ تحديث الصورة (لو تم رفع واحدة جديدة)
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/destinations");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + Path.GetExtension(dto.ImageFile.FileName);
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(stream);
                }

                destination.ImageCover = $"images/destnations/{fileName}";
            }

            // ✅ تحديث الحالة
            destination.IsActive = dto.IsActive;
            destination.ReferenceName = dto.ReferenceName;
            destination.MetaDescription = dto.MetaDescription;
            destination.MetaKeyWords = dto.MetaKeyWords;
            destination.Title = dto.Title;
            destination.Description = dto.Description;



            // ✅ تحديث الكيان
            unitOfWork.Repository<Destination>().Update(destination);
            // ✅ حفظ التغييرات
            await unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteDest(int id)
        {
            var spec = new DestnationForUpdateSpec(id);

            var destination = unitOfWork.Repository<Destination>().GetByIdSpecTEntityAsync(spec);
            if (destination == null)
            {
                return false;
            }

            unitOfWork.Repository<Destination>().Delete(destination);

            await unitOfWork.CompleteAsync();

            return true;
        }

    
    }
}

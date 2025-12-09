using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Store.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TourSite.Core;
using TourSite.Core.DTOs.HighLights;
using TourSite.Core.DTOs.Includes;
using TourSite.Core.DTOs.NotIncludes;
using TourSite.Core.DTOs.TourImg;
using TourSite.Core.DTOs.Tours;
using TourSite.Core.DTOs.Transfer;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;
using TourSite.Core.Specification.TourImgs;
using TourSite.Core.Specification.Tours;
using static System.Runtime.InteropServices.JavaScript.JSType;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;


namespace TourSite.Service.Services.Tours
{
    public class TourService: IToursService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public IWebHostEnvironment env { get; }


        public TourService(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment _env)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            env = _env;
        }


        public async Task<PageinationResponse<TourAllDto>> GetAllToursAsync(TourSpecParams specParams)
        {

            var spec = new TourSpecification(specParams);
            var allData = await unitOfWork.Repository<Tour>().GetAllSpecAsync(spec);

            if (allData is null) return null;

            // ✅ استخدم AutoMapper مباشرة مع الـ language
            var data = mapper.Map<IEnumerable<TourAllDto>>(allData);

            // Count بدون ترجمات
            var CountSpec = new TourWithCountSpecifications(specParams);
            var Count = await unitOfWork.Repository<Tour>().GetCountAsync(CountSpec);

            return new PageinationResponse<TourAllDto>(specParams.pageIndex, specParams.pageSize, Count, data);
        }

        public async Task<TourDto> GetTourByIdAsync(int id)
        {
            var spec = new TourSpecification(id);
            var Tour = await unitOfWork.Repository<Tour>().GetByIdSpecAsync(spec);

            return mapper.Map<TourDto>(Tour);
        }

        public async Task<PageinationResponse<TourAllDto>> GetAllToursTrueAsync(TourSpecParams tourSpecParams)
        {
         

            var spec = new TourSpecificationForAdmin(tourSpecParams);
            var allData = await unitOfWork.Repository<Tour>().GetAllSpecAsync(spec);

            if (allData is null) return null;

            // ✅ استخدم AutoMapper مباشرة مع الـ language
            var data = mapper.Map<IEnumerable<TourAllDto>>(allData);

            // Count بدون ترجمات
            var CountSpec = new TourWithCountSpecifications(tourSpecParams);
            var Count = await unitOfWork.Repository<Tour>().GetCountAsync(CountSpec);

            return new PageinationResponse<TourAllDto>(tourSpecParams.pageIndex, tourSpecParams.pageSize, Count, data);
        }


        public async Task AddTourAsync(TourCreateDto TourCreateDto)
        {

            if (!string.IsNullOrEmpty(TourCreateDto.IncludesJson))
            {
                TourCreateDto.IncludesPoints = JsonSerializer.Deserialize<List<TourIncludedDto>>(TourCreateDto.IncludesJson);
            }
            if (!string.IsNullOrEmpty(TourCreateDto.NonIncludesJson))
            {
                TourCreateDto.NonIncludesPoints = JsonSerializer.Deserialize<List<TourNotIncludedDto>>(TourCreateDto.NonIncludesJson);
            }
            if (!string.IsNullOrEmpty(TourCreateDto.hightlightJson))
            {
                TourCreateDto.hightlightPoints = JsonSerializer.Deserialize<List<TourHighlightDto>>(TourCreateDto.hightlightJson);
            }




            // 🖼️ حفظ الصورة في wwwroot/images/categoryTours
            string imagePath = string.Empty;
            if (TourCreateDto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/tours");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + Path.GetExtension(TourCreateDto.ImageFile.FileName);
                string fullPath = Path.Combine(uploadDir, fileName);

                // فتح الصورة باستخدام ImageSharp
                using (var image = await Image.LoadAsync(TourCreateDto.ImageFile.OpenReadStream()))
                {
                    // تغيير الأبعاد (مثلاً 800x600) مع عمل Stretch
                    image.Mutate(x => x.Resize(1600, 900));

                    // حفظ الصورة
                    await image.SaveAsync(fullPath, new JpegEncoder() { Quality = 90 });
                }

                imagePath = $"images/tours/{fileName}";
            }



            // 🧩 إنشاء الكيان
            var tour = new Tour
            {
                Title = TourCreateDto.Title,
                ImageCover = imagePath,
                IsActive = TourCreateDto.IsActive,
                FK_DestinationID = TourCreateDto.FK_DestinationID,
                FK_CategoryID = TourCreateDto.FK_CategoryID,
                FK_UserID = TourCreateDto.FK_UserID,
                StartLocation = TourCreateDto.StartLocation,
                EndLocation = TourCreateDto.EndLocation,
                Price = TourCreateDto.Price,
                Duration = TourCreateDto.Duration,
                LanguageOptions = TourCreateDto.LanguageOptions,
                Description = TourCreateDto.Description,
                CreatedAt = DateTime.Now,
                referenceName = TourCreateDto.referenceName,
                MetaDescription = TourCreateDto.MetaDescription,
                MetaKeyWords = TourCreateDto.MetaKeyWords,
                LinkVideo = TourCreateDto.LinkVideo,
                TourImgs  = new List<TourImg>(),
                Highlights = TourCreateDto.hightlightPoints.Select(h => new TourHighlight
                {
                    Text = h.Text
                }).ToList(),
                Includeds = TourCreateDto.IncludesPoints.Select(i => new TourIncluded
                {
                    Text = i.Text
                }).ToList(),
                NotIncludeds = TourCreateDto.NonIncludesPoints.Select(n => new TourNotIncluded
                {
                    Text = n.Text
                }).ToList(),

            };

            await unitOfWork.Repository<Tour>().AddAsync(tour);
            await unitOfWork.CompleteAsync();

            // ===============================
            //   رفع صور متعددة ImagesList
            // ===============================
            if (TourCreateDto.ImagesList != null && TourCreateDto.ImagesList.Any())
            {
                foreach (var imgDto in TourCreateDto.ImagesList)
                {
                    if (imgDto.ImageFile == null)
                        continue;

                    // رفع الصورة
                    string uploadDir = Path.Combine(env.WebRootPath, "images/tourImgs");
                    Directory.CreateDirectory(uploadDir);

                    string fileName = Guid.NewGuid() + Path.GetExtension(imgDto.ImageFile.FileName);
                    string fullPath = Path.Combine(uploadDir, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await imgDto.ImageFile.CopyToAsync(stream);
                    }

                    string imageUrl = $"images/tourImgs/{fileName}";


                    // إنشاء كيان الصورة
                    var tourImg = new TourImg
                    {
                        FK_TourId = tour.Id,
                        IsActive = imgDto.IsActive,
                        ImageCarouselUrl = imageUrl,
                        ReferenceName = imgDto.ReferenceName,
                        Title = imgDto.Title

                    };

                    await unitOfWork.Repository<TourImg>().AddAsync(tourImg);
                }

                await unitOfWork.CompleteAsync();
            }

        }


        public async Task<bool> UpdateTour(TourCreateDto dto, int id)
        {
            // استخدم الـ spec الجديد
            var spec = new TourForUpdateSpec(id);
            var tour = await unitOfWork.Repository<Tour>().GetByIdSpecTEntityTourAsync(spec);

            if (tour == null)
            {
                return false;
            }


            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };


            dto.IncludesPoints = !string.IsNullOrEmpty(dto.IncludesJson)
                ? JsonSerializer.Deserialize<List<TourIncludedDto>>(dto.IncludesJson, jsonOptions) ?? new List<TourIncludedDto>()
                : new List<TourIncludedDto>();

            dto.NonIncludesPoints = !string.IsNullOrEmpty(dto.NonIncludesJson)
                ? JsonSerializer.Deserialize<List<TourNotIncludedDto>>(dto.NonIncludesJson, jsonOptions) ?? new List<TourNotIncludedDto>()
                : new List<TourNotIncludedDto>();

            dto.hightlightPoints = !string.IsNullOrEmpty(dto.hightlightJson)
                ? JsonSerializer.Deserialize<List<TourHighlightDto>>(dto.hightlightJson, jsonOptions) ?? new List<TourHighlightDto>()
                : new List<TourHighlightDto>();



            // ✅ تحديث الصورة (لو تم رفع واحدة جديدة)
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/tour");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + Path.GetExtension(dto.ImageFile.FileName);
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    dto.ImageFile.CopyTo(stream);
                }

                tour.ImageCover = $"images/tour/{fileName}";
            }

            // ✅ تحديث الحالة
            tour.IsActive = dto.IsActive;
            tour.FK_CategoryID = dto.FK_CategoryID;
            tour.FK_DestinationID = dto.FK_DestinationID;
            tour.Price = dto.Price;
            tour.Duration = dto.Duration;
            tour.StartLocation = dto.StartLocation;
            tour.EndLocation = dto.EndLocation;
            tour.LanguageOptions = dto.LanguageOptions;
            tour.referenceName = dto.referenceName;
            tour.MetaDescription = dto.MetaDescription;
            tour.MetaKeyWords = dto.MetaKeyWords;
            tour.LinkVideo = dto.LinkVideo;
            tour.Title = dto.Title;
            tour.Description = dto.Description;




            tour.Includeds = tour.Includeds ?? new List<TourIncluded>();
            foreach (var dtoItem in dto.IncludesPoints)
            {
                if (string.IsNullOrWhiteSpace(dtoItem.Text)) continue;

                var existing = tour.Includeds.FirstOrDefault(p => p.Id == dtoItem.Id);
                if (existing != null)
                {
                    existing.Text = dtoItem.Text;
                }
                else
                {
                    tour.Includeds.Add(new TourIncluded
                    {
                        TourId = tour.Id,
                        Text = dtoItem.Text
                    });
                }
            }


            tour.NotIncludeds = tour.NotIncludeds ?? new List<TourNotIncluded>();
            foreach (var dtoItem in dto.NonIncludesPoints)
            {
                if (string.IsNullOrWhiteSpace(dtoItem.Text)) continue;

                var existing = tour.NotIncludeds.FirstOrDefault(p => p.Id == dtoItem.Id);
                if (existing != null)
                {
                    existing.Text = dtoItem.Text;
                }
                else
                {
                    tour.NotIncludeds.Add(new TourNotIncluded
                    {
                        TourId = tour.Id,
                        Text = dtoItem.Text
                    });
                }
            }


            tour.Highlights = tour.Highlights ?? new List<TourHighlight>();
            foreach (var dtoItem in dto.hightlightPoints)
            {
                if (string.IsNullOrWhiteSpace(dtoItem.Text)) continue;

                var existing = tour.Highlights.FirstOrDefault(p => p.Id == dtoItem.Id);
                if (existing != null)
                {
                    existing.Text = dtoItem.Text;
                }
                else
                {
                    tour.Highlights.Add(new TourHighlight
                    {
                        TourId = tour.Id,
                        Text = dtoItem.Text
                    });
                }
            }
            // ✅ تحديث الكيان
            unitOfWork.Repository<Tour>().Update(tour);
            // ✅ حفظ التغييرات
            await unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteTour(int id)
        {
            var spec = new TourForUpdateSpec(id);

            var tour = unitOfWork.Repository<Tour>().GetByIdSpecTEntityAsync(spec);
            if (tour == null)
            {
                return false;
            }

            unitOfWork.Repository<Tour>().Delete(tour);

            await unitOfWork.CompleteAsync();

            return true;
        }

    }

}

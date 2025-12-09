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
using TourSite.Core.DTOs.Destnation;
using TourSite.Core.DTOs.HighLights;
using TourSite.Core.DTOs.Includes;
using TourSite.Core.DTOs.NotIncludes;
using TourSite.Core.DTOs.Tours;
using TourSite.Core.DTOs.Transfer;
using TourSite.Core.DTOs.User;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;
using TourSite.Core.Specification.Destnations;

using TourSite.Core.Specification.Transfers;
using TourSite.Core.Specification.Users;

namespace TourSite.Service.Services.Trasnfers
{
    public class TransferService : ITransferService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public IWebHostEnvironment env { get; }


        public TransferService(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment _env)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            env = _env;
        }



        public async Task<PageinationResponse<TransferAllDto>> GetAllTransToursAsync(TrasferSpecParam specParams)
        {
           

            var spec = new TransferSpecification(specParams);
            var allData = await unitOfWork.Repository<Transfer>().GetAllSpecAsync(spec);

            if (allData is null) return null;

            // ✅ استخدم AutoMapper مباشرة مع الـ language
            var data = mapper.Map<IEnumerable<TransferAllDto>>(allData);

            // Count بدون ترجمات
            var CountSpec = new TransferWithCountSpecifications(specParams);
            var Count = await unitOfWork.Repository<Transfer>().GetCountAsync(CountSpec);

            return new PageinationResponse<TransferAllDto>(specParams.pageIndex, specParams.pageSize, Count, data);

        }


        public async Task<TransferDto> GetCatTransByIdAsync(int id)
        {
            var spec = new TransferSpecification(id);
            var CategoryTour = await unitOfWork.Repository<Transfer>().GetByIdSpecAsync(spec);

            return mapper.Map<TransferDto>(CategoryTour);
        }


        public async Task AddTransferAsync(TransferCreateDto TransferDto)
        {
            if (!string.IsNullOrEmpty(TransferDto.PriecesListJson))
            {
                TransferDto.PricesList = JsonSerializer.Deserialize<List<TransferPricesDTO>>(TransferDto.PriecesListJson);
            }
            if (!string.IsNullOrEmpty(TransferDto.IncludesListJson))
            {
                TransferDto.IncludesList = JsonSerializer.Deserialize<List<TransferIncludedDto>>(TransferDto.IncludesListJson);
            }

            if (!string.IsNullOrEmpty(TransferDto.NotIncludedListJson))
            {
                TransferDto.NotIncludedList = JsonSerializer.Deserialize<List<TransferNotIncludedDto>>(TransferDto.NotIncludedListJson);
            }
            if (!string.IsNullOrEmpty(TransferDto.HighlightListJson))
            {
                TransferDto.HighlightList = JsonSerializer.Deserialize<List<TransferHighlightDto>>(TransferDto.HighlightListJson);
            }

            // 🖼️ حفظ الصورة في wwwroot/images/categoryTours
            string imagePath = string.Empty;
            if (TransferDto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/transfers");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + Path.GetExtension(TransferDto.ImageFile.FileName);
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await TransferDto.ImageFile.CopyToAsync(stream);
                }

                imagePath = $"images/transfers/{fileName}";
            }


            // 🧩 إنشاء الكيان
            var transfer = new Transfer
            {
                ImageCover = imagePath,
                IsActive = TransferDto.IsActive,
                FK_DestinationID = TransferDto.FK_DestinationID,
                ReferenceName = TransferDto.ReferenceName,
                Title = TransferDto.Title,
                Description = TransferDto.Description,
                MetaDescription = TransferDto.MetaDescription,
                MetaKeyWords = TransferDto.MetaKeyWords,

                PricesList = TransferDto.PricesList.Select(t => new TrasnferPrices
                {
                    Title = t.Title,
                    PrivtePrice = t.PrivtePrice,
                    SharedPrice = t.SharedPrice
                }).ToList(),
                Includeds = TransferDto.IncludesList.Select(i => new TransferIncluded
                {
                    Text = i.Text
                }).ToList(),
                NotIncludeds = TransferDto.NotIncludedList.Select(n => new TransferNotIncluded
                {
                    Text = n.Text
                }).ToList(),
                Highlights = TransferDto.HighlightList.Select(h => new TransferHighlight
                {
                    Text = h.Text
                }).ToList()

            };

            await unitOfWork.Repository<Transfer>().AddAsync(transfer);
            await unitOfWork.CompleteAsync();
        }

        public async Task<bool> UpdateTransfer(TransferCreateDto dto, int id)
        {
            // جلب الـ Transfer مع الـ spec
            var spec = new TransferForUpdateSpec(id);
            var transfer =  unitOfWork.Repository<Transfer>().GetByIdSpecTEntityAsync(spec);

            if (transfer == null) return false;

            // -----------------------------
            // 1️⃣ Deserialize JSON مع حماية null
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            dto.PricesList = !string.IsNullOrEmpty(dto.PriecesListJson)
                ? JsonSerializer.Deserialize<List<TransferPricesDTO>>(dto.PriecesListJson, jsonOptions) ?? new List<TransferPricesDTO>()
                : new List<TransferPricesDTO>();

            dto.IncludesList = !string.IsNullOrEmpty(dto.IncludesListJson)
                ? JsonSerializer.Deserialize<List<TransferIncludedDto>>(dto.IncludesListJson, jsonOptions) ?? new List<TransferIncludedDto>()
                : new List<TransferIncludedDto>();

            dto.NotIncludedList = !string.IsNullOrEmpty(dto.NotIncludedListJson)
                ? JsonSerializer.Deserialize<List<TransferNotIncludedDto>>(dto.NotIncludedListJson, jsonOptions) ?? new List<TransferNotIncludedDto>()
                : new List<TransferNotIncludedDto>();

            dto.HighlightList = !string.IsNullOrEmpty(dto.HighlightListJson)
                ? JsonSerializer.Deserialize<List<TransferHighlightDto>>(dto.HighlightListJson, jsonOptions) ?? new List<TransferHighlightDto>()
                : new List<TransferHighlightDto>();

            // -----------------------------
            // 2️⃣ تحديث الصورة إذا تم رفع واحدة جديدة
            if (dto.ImageFile != null)
            {
                string uploadDir = Path.Combine(env.WebRootPath, "images/transfers");
                Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid() + Path.GetExtension(dto.ImageFile.FileName);
                string fullPath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(stream);
                }

                transfer.ImageCover = $"images/transfers/{fileName}";
            }

            // -----------------------------
            // 3️⃣ تحديث الحقول الأساسية
            transfer.IsActive = dto.IsActive;
            transfer.FK_DestinationID = dto.FK_DestinationID;
            transfer.ReferenceName = dto.ReferenceName;
            transfer.Description = dto.Description;
            transfer.Title = dto.Title;
            transfer.MetaDescription = dto.MetaDescription;
            transfer.MetaKeyWords = dto.MetaKeyWords;

            // -----------------------------
            // 4️⃣ تحديث PricesList
            transfer.PricesList = transfer.PricesList ?? new List<TrasnferPrices>();
            foreach (var dtoItem in dto.PricesList)
            {
                if (string.IsNullOrWhiteSpace(dtoItem.Title)) continue; // حماية من null/empty

                var existing = transfer.PricesList.FirstOrDefault(p => p.Id == dtoItem.Id);
                if (existing != null)
                {
                    existing.Title = dtoItem.Title;
                    existing.PrivtePrice = dtoItem.PrivtePrice;
                    existing.SharedPrice = dtoItem.SharedPrice;
                }
                else
                {
                    transfer.PricesList.Add(new TrasnferPrices
                    {
                        TransferId = transfer.Id,
                        Title = dtoItem.Title,
                        PrivtePrice = dtoItem.PrivtePrice,
                        SharedPrice = dtoItem.SharedPrice
                    });
                }
            }

            // -----------------------------
            // 5️⃣ تحديث Includeds
            transfer.Includeds = transfer.Includeds ?? new List<TransferIncluded>();
            foreach (var dtoItem in dto.IncludesList)
            {
                if (string.IsNullOrWhiteSpace(dtoItem.Text)) continue;

                var existing = transfer.Includeds.FirstOrDefault(p => p.Id == dtoItem.Id);
                if (existing != null)
                {
                    existing.Text = dtoItem.Text;
                }
                else
                {
                    transfer.Includeds.Add(new TransferIncluded
                    {
                        TransferId = transfer.Id,
                        Text = dtoItem.Text
                    });
                }
            }

            // -----------------------------
            // 6️⃣ تحديث NotIncludeds
            transfer.NotIncludeds = transfer.NotIncludeds ?? new List<TransferNotIncluded>();
            foreach (var dtoItem in dto.NotIncludedList)
            {
                if (string.IsNullOrWhiteSpace(dtoItem.Text)) continue;

                var existing = transfer.NotIncludeds.FirstOrDefault(p => p.Id == dtoItem.Id);
                if (existing != null)
                {
                    existing.Text = dtoItem.Text;
                }
                else
                {
                    transfer.NotIncludeds.Add(new TransferNotIncluded
                    {
                        TransferId = transfer.Id,
                        Text = dtoItem.Text
                    });
                }
            }

            // -----------------------------
            // 7️⃣ تحديث Highlights
            transfer.Highlights = transfer.Highlights ?? new List<TransferHighlight>();
            foreach (var dtoItem in dto.HighlightList)
            {
                if (string.IsNullOrWhiteSpace(dtoItem.Text)) continue;

                var existing = transfer.Highlights.FirstOrDefault(p => p.Id == dtoItem.Id);
                if (existing != null)
                {
                    existing.Text = dtoItem.Text;
                }
                else
                {
                    transfer.Highlights.Add(new TransferHighlight
                    {
                        TransferId = transfer.Id,
                        Text = dtoItem.Text
                    });
                }
            }

            // -----------------------------
            // 8️⃣ حفظ التغييرات
            unitOfWork.Repository<Transfer>().Update(transfer);
            await unitOfWork.CompleteAsync();

            return true;
        }


        public async Task<bool> Deletetransfer(int id)
        {
            var spec = new TransferForUpdateSpec(id);

            var transfer = unitOfWork.Repository<Transfer>().GetByIdSpecTEntityAsync(spec);
            if (transfer == null)
            {
                return false;
            }

            unitOfWork.Repository<Transfer>().Delete(transfer);

            await unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<PageinationResponse<TransferAllDto>> GetAllTransToursAdminAsync(TrasferSpecParam specParams)
        {
         

            var spec = new TrasnsferSpecificationForAdmin(specParams);
            var allData = await unitOfWork.Repository<Transfer>().GetAllSpecAsync(spec);

            if (allData is null) return null;

            // ✅ استخدم AutoMapper مباشرة مع الـ language
            var data = mapper.Map<IEnumerable<TransferAllDto>>(allData);

            // Count بدون ترجمات
            var CountSpec = new TransferWithCountSpecifications(specParams);
            var Count = await unitOfWork.Repository<Transfer>().GetCountAsync(CountSpec);

            return new PageinationResponse<TransferAllDto>(specParams.pageIndex, specParams.pageSize, Count, data);
        }
    }
}
using AutoMapper;
using Sword.Application.Dtos;
using Sword.Core;
using Sword.Domain;
using Sword.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Sword.Application
{
    public class SwordProfile : Profile
    {
        public static readonly Dictionary<bool, string> AVAILABLE =
            new Dictionary<bool, string>
            {
                { true, "TRUE" },
                { false, "FALSE" }
            };

        public static readonly Dictionary<string, string> ICON =
            new Dictionary<string, string> {
                { "form", "FORM" }
            };

        public static readonly Dictionary<UserType, string> USERTYPE =
            new Dictionary<UserType, string>
            {
                { UserType.MANAGER, "MANAGER" },
                { UserType.MEMBER, "MEMBER" }
            };

        public SwordProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(
                    dest => dest.Id,
                    opts => opts.MapFrom(src => src.UserId))
                .ForMember(
                    dest => dest.AvailableDisplay,
                    opts => opts.MapFrom(src => AVAILABLE[src.Available]));

            CreateMap<UserCreateDto, User>()
                .ForMember(
                    dest => dest.UserId,
                    opts => opts.MapFrom(src => Guid.NewGuid()));

            CreateMap<User, UserUpdateDto>()
                .ForMember(
                    dest => dest.Id,
                    opts => opts.MapFrom(src => src.UserId))
                .ForMember(
                    dest => dest.Version,
                    opts => opts.MapFrom(src => src.Version.ToHexString()));

            CreateMap<UserUpdateDto, User>()
                .ForMember(
                    dest => dest.UserId,
                    opts => opts.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.Version,
                    opts => opts.Ignore());
        }
    }
}

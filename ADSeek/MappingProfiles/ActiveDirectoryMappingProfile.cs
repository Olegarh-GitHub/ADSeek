using System;
using ADSeek.Domain.Extensions;
using ADSeek.Domain.Models;
using AutoMapper;

namespace ADSeek.MappingProfiles
{
    public class ActiveDirectoryMappingProfile : Profile
    {
        public ActiveDirectoryMappingProfile()
        {
            CreateMap<ActiveDirectoryObject, ActiveDirectoryComputer>()
                .ForMember
                (
                    dest => dest.DistinguishedName,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("distinguishedName")
                                ? src.Attributes.GetAttribute("distinguishedName").StringValue
                                : null
                        )
                )
                .ForMember
                (
                    dest => dest.SAMAccountName,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("sAMAccountName")
                                ? src.Attributes.GetAttribute("sAMAccountName").StringValue
                                : null
                        )
                )
                .ForMember
                (
                    dest => dest.OperatingSystem,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("operatingSystem")
                                ? src.Attributes.GetAttribute("distinguishedName").StringValue
                                : null
                        )
                )
                .ForMember
                (
                    dest => dest.OperatingSystemVersion,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("operatingSystemVersion")
                                ? src.Attributes.GetAttribute("distinguishedName").StringValue
                                : null
                        )
                )
                .ForMember
                (
                    dest => dest.DNSHostName,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("dNSHostName")
                                ? src.Attributes.GetAttribute("dNSHostName").StringValue
                                : null
                        )
                )
                .ForMember
                (
                    dest => dest.ObjectGuid,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("objectGUID")
                                ? new Guid(src.Attributes.GetAttribute("objectGUID").ByteValue)
                                : Guid.Empty
                        )
                )
                ;
            
            CreateMap<ActiveDirectoryObject, ActiveDirectoryGroup>()
                .ForMember
                (
                    dest => dest.DistinguishedName,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("distinguishedName")
                                ? src.Attributes.GetAttribute("distinguishedName").StringValue
                                : null
                        )
                )
                .ForMember
                (
                    dest => dest.Members,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("member")
                                ? src.Attributes.GetAttribute("member").StringValueArray
                                : null
                        )
                )
                .ForMember
                (
                    dest => dest.SAMAccountName,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("sAMAccountName")
                                ? src.Attributes.GetAttribute("sAMAccountName").StringValue
                                : null
                        )
                )
                .ForMember
                (
                    dest => dest.ObjectGuid,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("objectGUID")
                                ? new Guid(src.Attributes.GetAttribute("objectGUID").ByteValue)
                                : Guid.Empty
                        )
                );
            
            CreateMap<ActiveDirectoryObject, ActiveDirectoryUser>()
                .ForMember
                (
                    dest => dest.DistinguishedName,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("distinguishedName")
                                ? src.Attributes.GetAttribute("distinguishedName").StringValue
                                : null
                        )
                )
                .ForMember
                (
                    dest => dest.Initials,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("initials")
                                ? src.Attributes.GetAttribute("initials").StringValue
                                : null
                        )
                )
                .ForMember
                (
                    dest => dest.Mail,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("mail")
                                ? src.Attributes.GetAttribute("mail").StringValue
                                : null
                        )
                )
                .ForMember
                (
                    dest => dest.Photo,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("photo")
                                ? src.Attributes.GetAttribute("photo").ByteValue
                                : null
                        )
                )
                .ForMember
                (
                    dest => dest.Surname,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("sn")
                                ? src.Attributes.GetAttribute("sn").StringValue
                                : null
                        )
                )
                .ForMember
                (
                    dest => dest.CommonName,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("cn")
                                ? src.Attributes.GetAttribute("cn").StringValue
                                : null
                        )
                )
                .ForMember
                (
                    dest => dest.MemberOf,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("memberOf")
                                ? src.Attributes.GetAttribute("memberOf").StringValueArray
                                : null
                        )
                )
                .ForMember
                (
                    dest => dest.ObjectClass,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("objectClass")
                                ? src.Attributes.GetAttribute("objectClass").ToObjectClass()
                                : default
                        )
                )
                .ForMember
                (
                    dest => dest.ObjectGuid,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("objectGUID")
                                ? new Guid(src.Attributes.GetAttribute("objectGUID").ByteValue)
                                : Guid.Empty
                        )
                )
                .ForMember
                (
                    dest => dest.UserAccountControl,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("userAccountControl")
                                ? src.Attributes.GetAttribute("userAccountControl").ToUserAccountControl()
                                : default
                        )
                ).ForMember
                (
                    dest => dest.SamAccountName,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("sAMAccountName")
                                ? src.Attributes.GetAttribute("sAMAccountName").StringValue
                                : null
                        )
                )
                .ForMember
                (
                    dest => dest.GivenName,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("givenName")
                                ? src.Attributes.GetAttribute("givenName").StringValue
                                : null
                        )
                )
                .ForMember
                (
                    dest => dest.DisplayName,
                    opt => opt
                        .MapFrom
                        (
                            src => src.Attributes.ContainsKey("displayName")
                                ? src.Attributes.GetAttribute("displayName").StringValue
                                : null
                        )
                )
                ;
        }
    }
}
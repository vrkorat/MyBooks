using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using AutoMapper;
using UniqueBooks.Dtos;
using UniqueBooks.Models;

namespace UniqueBooks.App_Start
{
    public class MappingProfile : Profile
    {
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
            IMappingExpression<TSource, TDestination> map,
            Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, config => config.Ignore());
            return map;
        }

        public MappingProfile()
        {


            
            //Customer Mapping

            //Domian to Dto

            //Basically when your API goes outbound to send stuff to people you'll always pass your Objects through their
            //specific Dto. So it has to map Customer -> CustomerDto.
            //API -> outbound
            Mapper.CreateMap<Customer, CustomerDto>();

            Mapper.CreateMap<Book, BookDto>();

            Mapper.CreateMap<MembershipType, MembershipTypeDto>();

            Mapper.CreateMap<Genre, GenerDto>();
            
            //Dto to Domain

            //when your API gets stuff sent by people, all the data passes through the Dto first, and then to the Customer
            //Object.
            //you'd say to your AutoMapper
            //"Hey, don't worry about the id, don't map that."
            //Api <- inbound

            Mapper.CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore()); 


            

            //why i ignore Id in Dto while mapping because same as above
            //why i ignore DateAdd in Dto while mapping because while Creating book 
            //i explicitly use the prop. of Book.DateAdded to set value and then it should not change further
            Mapper.CreateMap<BookDto, Book>()
                .ForMember( b => b.Id,opt => opt.Ignore())
                .ForMember(b => b.DateAdded, opt =>opt.Ignore());


        }
    }
}
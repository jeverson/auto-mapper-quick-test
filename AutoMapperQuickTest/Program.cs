using AutoMapper;
using System;
using System.Collections.Generic;

namespace AutoMapperQuickTest
{
    class Program
    {
        class Model
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public DateTime DOB { get; set; }
        }

        class ViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime DayOfBirth { get; set; }
        }

        static void Main(string[] args)
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Model, ViewModel>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName))
                    .ForMember(dest => dest.DayOfBirth, opt => opt.MapFrom(src => src.DOB));

                cfg.CreateMap<ViewModel, Model>()
                    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.DOB, opt => opt.MapFrom(src => src.DayOfBirth));

            });

            var source = new Model { Id = 1, FullName = "First Test", DOB = DateTime.Now.AddYears(-18) };

            var result = Mapper.Map<Model, ViewModel>(source);
            Print(result);

            var newModel = Mapper.Map<ViewModel, Model>(result);
            Print(newModel);

            var sourceList = new List<Model> { source };
            var destList = Mapper.Map<List<Model>, List<ViewModel>>(sourceList);
            destList.ForEach(m => Print(m));

            Console.Read();
        }

        private static void Print(ViewModel model)
        {
            WriteOut(model.Id, model.Name, model.DayOfBirth);
        }

        private static void Print(Model model)
        {
            WriteOut(model.Id, model.FullName, model.DOB);
        }

        private static void WriteOut(int id, string name, DateTime dob)
        {
            Console.WriteLine($"Id:  {id}, Name: {name}, DOB: {dob}");
        }
    }
}

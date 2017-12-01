﻿using System.Collections.Generic;
using System.Linq;
using CZS.Web.Data;
using CZS.Web.Data.Entities;
using DotNetify;

namespace CZS.Web.ViewModels
{
    public class FeaturesViewModel: BaseVM
    {
        public string Skills_itemkey => nameof(ProgressionSkills.SkillId);
        public IEnumerable<dynamic> Skills
        {
            get => Get<IEnumerable<dynamic>>();
            set => Set(value);
        }

        public string Abilities_itemkey => nameof(Data.Entities.Abilities.AbilityId);
        public IEnumerable<dynamic> Abilities
        {
            get => Get<IEnumerable<dynamic>>();
            set => Set(value);
        }


        public FeaturesViewModel(DataContext db)
        {
            Skills = db.ProgressionSkills.Where(x => !x.IsDisabled).OrderBy(x => x.Name)
                .Select(x => new
                {
                    SkillID = x.SkillId,
                    x.Name,
                    x.Description
                }).ToList();

            Abilities = db.Abilities.Where(x => x.IsActive).OrderBy(x => x.Name)
                .Select(x => new
                {
                    AbilityID = x.AbilityId,
                    x.Name,
                    x.Description
                }).ToList();
        }
    }
}

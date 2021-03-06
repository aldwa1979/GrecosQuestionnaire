﻿using GrecosQuestionnaire.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Models
{
    public class PartnerEquals : IEqualityComparer<PartnerUserViewModel>
    {
        public bool Equals(PartnerUserViewModel left, PartnerUserViewModel right)
        {
            if (left == null && right == null)
            {
                return true;
            }
            if (left == null || right == null)
            {
                return false;
            }
            return left.PartnerId == right.PartnerId;
        }

        public int GetHashCode(PartnerUserViewModel author)
        {
            return (author.PartnerId).GetHashCode();
        }
    }
}

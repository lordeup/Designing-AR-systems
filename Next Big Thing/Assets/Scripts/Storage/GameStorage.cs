using System.Collections.Generic;
using Card;
using Card.Type;
using UnityEngine;

namespace Storage
{
    public class GameStorage : MonoBehaviour
    {
        private readonly List<FounderCard> _founderCards = new();
        private readonly List<CompanyCard> _companyCards = new();

        public GameStorage()
        {
            InitializeFounderCards();
            InitializeCompanyCards();
        }

        public FounderCard GetFounderCardByType(FounderCardType type)
        {
            return _founderCards.Find(item => item.Type == type);
        }

        public CompanyCard GetCompanyCardByType(CompanyCardType type)
        {
            return _companyCards.Find(item => item.Type == type);
        }

        private void InitializeFounderCards()
        {
            _founderCards.Add(new FounderCard(FounderCardType.CollegeDropout, SuperPowerSkill.Programming, 20000));
            _founderCards.Add(new FounderCard(FounderCardType.FormerDesigner, SuperPowerSkill.Design, 20000));
            _founderCards.Add(new FounderCard(FounderCardType.FormerAccountant, SuperPowerSkill.Accounting, 20000));
            _founderCards.Add(new FounderCard(FounderCardType.FormerLawyer, SuperPowerSkill.Legal, 40000));
            _founderCards.Add(new FounderCard(FounderCardType.FormerConsultant, SuperPowerSkill.Marketing, 0));
            _founderCards.Add(
                new FounderCard(FounderCardType.FormerPublicRelations, SuperPowerSkill.PublicRelations, 0));
            _founderCards.Add(new FounderCard(FounderCardType.FormerSalesExec, SuperPowerSkill.Sales, 0));
            _founderCards.Add(new FounderCard(FounderCardType.SerialEntrepreneur, SuperPowerSkill.YourChoice, 100000));
            _founderCards.Add(new FounderCard(FounderCardType.CollegeStudent, SuperPowerSkill.Design, 0));
            _founderCards.Add(new FounderCard(FounderCardType.Programmer, SuperPowerSkill.Programming, 40000));
            _founderCards.Add(new FounderCard(FounderCardType.FormerActress, SuperPowerSkill.Marketing, 0));
        }

        private void InitializeCompanyCards()
        {
            _companyCards.Add(new CompanyCard(CompanyCardType.SocialNetwork, 40000));
            _companyCards.Add(new CompanyCard(CompanyCardType.SuperSecretScienceTech, 60000));
            _companyCards.Add(new CompanyCard(CompanyCardType.Transportation, 60000));
            _companyCards.Add(new CompanyCard(CompanyCardType.EducationTech, 40000));
            _companyCards.Add(new CompanyCard(CompanyCardType.EntertainmentGaming, 60000));
            _companyCards.Add(new CompanyCard(CompanyCardType.MediaMarketing, 40000));
            _companyCards.Add(new CompanyCard(CompanyCardType.RealEstate, 60000));
            _companyCards.Add(new CompanyCard(CompanyCardType.Retail, 40000));
            _companyCards.Add(new CompanyCard(CompanyCardType.BusinessToConsumer, 40000));
            _companyCards.Add(new CompanyCard(CompanyCardType.BusinessToBusiness, 60000));
        }
    }
}

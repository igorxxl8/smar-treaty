using System;
using System.Collections.Generic;
using SmarTreaty.Common.Core.Helpers.Interfaces;
using SmarTreaty.Common.Core.Services.Interfaces;
using SmarTreaty.Common.DomainModel;

namespace SmarTreaty.Business.Services
{
    public class TemplateService : BaseService, ITemplateService
    {
        public TemplateService(IDatabaseWorkUnit db) : base(db)
        {
        }

        public void AddTemplate(Contract template)
        {
            throw new NotImplementedException();
        }

        public void DeleteTemplate(Contract template)
        {
            throw new NotImplementedException();
        }

        public void DeleteTemplate(Guid id)
        {
            throw new NotImplementedException();
        }

        public void EditeTemplate(Contract template)
        {
            throw new NotImplementedException();
        }

        public ICollection<Contract> GetTemplatesByEditor(Guid editorId)
        {
            throw new NotImplementedException();
        }

        public void PublishTemplate(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

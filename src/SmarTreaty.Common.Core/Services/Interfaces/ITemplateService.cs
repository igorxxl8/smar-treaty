using SmarTreaty.Common.DomainModel;
using System;
using System.Collections.Generic;

namespace SmarTreaty.Common.Core.Services.Interfaces
{
    public interface ITemplateService : IBaseService
    {
        ICollection<Contract> GetTemplatesByEditor(Guid editorId);
        void AddTemplate(Contract template);
        void EditeTemplate(Contract template);
        void DeleteTemplate(Contract template);
        void DeleteTemplate(Guid id);
        void PublishTemplate(Guid id);
    }
}

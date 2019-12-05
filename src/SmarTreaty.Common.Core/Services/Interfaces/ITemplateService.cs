using SmarTreaty.Common.DomainModel;
using System;
using System.Collections.Generic;

namespace SmarTreaty.Common.Core.Services.Interfaces
{
    public interface ITemplateService : IBaseService
    {
        ICollection<Template> GetTemplatesByEditor(Guid editorId);
        void AddTemplate(Template template);
        void EditeTemplate(Template template);
        void DeleteTemplate(Template template);
        void DeleteTemplate(Guid id);
        void PublishTemplate(Guid id);
    }
}

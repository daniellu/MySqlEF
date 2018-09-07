using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.AnalyteManagement.GuidelineCalculation
{
    public class ExccedanceDeciderFactory
    {
        private Dictionary<Type, IExccedanceDecider> Mapping { get; set; }

        public ExccedanceDeciderFactory()
        {
            Mapping = new Dictionary<Type, IExccedanceDecider>();
        }

        public IExccedanceDecider BuildDecider(Type type)
        {
            if (Mapping.ContainsKey(type))
            {
                return Mapping[type];
            }
            else
            {
                throw new KeyNotFoundException(type.Name + " is not a valid guideline value type");
            }
        }

        public ExccedanceDeciderFactory AddMapping(Type type, IExccedanceDecider decider)
        {
            Mapping.Add(type, decider);

            return this;
        }

        public static ExccedanceDeciderFactory Initial()
        {
            var factory = new ExccedanceDeciderFactory();
            factory.AddMapping(typeof(SimpleGuidelineValue), new SimpleGuidelineValueExccedanceDecider())
                .AddMapping(typeof(RangeGuidelineValue), new RangeGuidelineValueExccedanceDecider());

            return factory;
        }


    }
}

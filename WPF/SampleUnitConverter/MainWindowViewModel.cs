using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SampleUnitConverter {
    class MainWindowViewModel : BindableBase {
        private double metricValue = 0;
        private double imperialValue = 0;

        public double MetricValue {
            get => metricValue;
            set => SetProperty(ref metricValue, value);

        }
        public double ImperialValue {
            get => imperialValue;
            set => SetProperty(ref imperialValue, value);
        }

        public DelegateCommand ImperialUnitToMetric { get; private set; }
        public DelegateCommand MetricToImperialUnit { get; private set; }

        public MetricUnit? CurrentMetricUnit { get; set; }
        public ImperialUnit? CurrentInperialUnit { get; set; }

        public MainWindowViewModel() {
            ImperialUnitToMetric = new DelegateCommand(() => {
                if (CurrentMetricUnit == null || CurrentInperialUnit == null) return;
                MetricValue = CurrentMetricUnit.FromImperialUnit(CurrentInperialUnit, ImperialValue);
            });
            MetricToImperialUnit = new DelegateCommand(() => {
                if (CurrentMetricUnit == null || CurrentInperialUnit == null) return;
                ImperialValue = CurrentInperialUnit.FromMetricUnit(CurrentMetricUnit, MetricValue);
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.IO;

namespace Utilities
{
    public class GraphHelper
    {
        public MemoryStream CreateChart(int width, int height,IList<GraphModel> peoples, SeriesChartType chartType,string chartTitle,string legendTitle)
        {
            Chart chart = new Chart();
            chart.Width = width;
            chart.Height = height;
            chart.BackColor = Color.FromArgb(211, 223, 240);
            chart.BorderlineDashStyle = ChartDashStyle.Solid;
            chart.BackSecondaryColor = Color.White;
            chart.BackGradientStyle = GradientStyle.TopBottom;
            chart.BorderlineWidth = 1;
            chart.Palette = ChartColorPalette.BrightPastel;
            chart.BorderlineColor = Color.FromArgb(26, 59, 105);
            chart.RenderType = RenderType.BinaryStreaming;
            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            chart.AntiAliasing = AntiAliasingStyles.All;
            chart.TextAntiAliasingQuality = TextAntiAliasingQuality.Normal;
            chart.Titles.Add(CreateTitle(chartTitle));
            chart.Legends.Add(CreateLegend(legendTitle));
            chart.Series.Add(CreateSeries(peoples, chartType));
            chart.ChartAreas.Add(CreateChartArea());

            MemoryStream ms = new MemoryStream();
            chart.SaveImage(ms);
            return ms;
        }
        
        public Title CreateTitle(string chartTitle)
        {
            Title title = new Title();
            title.Text = chartTitle;
            title.ShadowColor = Color.FromArgb(32, 0, 0, 0);
            title.Font = new Font(new FontFamily("Khmer OS Battambang"), 14, FontStyle.Bold);//new Font("Trebuchet MS", 14F, FontStyle.Bold);
            title.ShadowOffset = 3;
            title.ForeColor = Color.FromArgb(26, 59, 105);

            return title;
        }

        public Legend CreateLegend(string legendTitle)
        {
            Legend legend = new Legend();
            legend.Name = "Legend";
            legend.Title = legendTitle;
            legend.Docking = Docking.Bottom;
            legend.Alignment = StringAlignment.Center;
            legend.BackColor = Color.Transparent;
            legend.Font = new Font(new FontFamily("Khmer OS Battambang"), 12); //new Font(new FontFamily("Trebuchet MS"), 9);
            legend.LegendStyle = LegendStyle.Row;

            return legend;
        }

        public Series CreateSeries(IList<GraphModel> results, SeriesChartType chartType)
        {
            Series seriesDetail = new Series();
            seriesDetail.Name = "Series";            
            seriesDetail.IsValueShownAsLabel = false;
            seriesDetail.Color = Color.FromArgb(198, 99, 99);
            seriesDetail.ChartType = chartType;
            seriesDetail.BorderWidth = 2;
            seriesDetail["DrawingStyle"] = "Cylinder";
            seriesDetail["PieDrawingStyle"] = "SoftEdge";
            DataPoint point;

            foreach (GraphModel result in results)
            {
                point = new DataPoint();
                point.AxisLabel = result.Label;
                point.YValues = new double[] { result.Value };
                seriesDetail.Points.Add(point);
            }
            seriesDetail.ChartArea = "Result Chart";

            return seriesDetail;
        }

        public ChartArea CreateChartArea()
        {
            ChartArea chartArea = new ChartArea();
            chartArea.Name = "Result Chart";
            chartArea.BackColor = Color.Transparent;
            chartArea.AxisX.IsLabelAutoFit = false;
            chartArea.AxisY.IsLabelAutoFit = false;
            chartArea.AxisX.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
            chartArea.AxisY.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
            chartArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.Interval = 1;
            // chartArea.Position.Width = 98;
            // chartArea.Position.Height = 70;
            // chartArea.Position.Y = 15;
            // chartArea.Position.X = 0;
            //chartArea.Area3DStyle.Enable3D = true;
            /*chartArea.Area3DStyle.Rotation = 10;
            chartArea.Area3DStyle.Perspective = 10;
            chartArea.Area3DStyle.Inclination = 15;
            chartArea.Area3DStyle.IsRightAngleAxes=false;
            chartArea.Area3DStyle.WallWidth=0;
            chartArea.Area3DStyle.IsClustered=false;*/
            return chartArea;
        }        
    }

    public class GraphModel{
        public string Label { get; set; }
        public double Value { get; set; }
    }
}

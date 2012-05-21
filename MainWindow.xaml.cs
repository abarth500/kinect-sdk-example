using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;

namespace kinect_sdk_example
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        KinectSensor kinect;

        public MainWindow()
        {
            InitializeComponent();
            //Kinectの初期化
            kinect = KinectSensor.KinectSensors[0];

            //イベントハンドラの登録
            kinect.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(handler_SkeletonFrameReady);
            //骨格トラッキングの有効化
            kinect.SkeletonStream.Enable();

            kinect.Start();
        }

        void handler_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e) {
            SkeletonFrame temp = e.OpenSkeletonFrame();
            if (temp != null)
            {
                Skeleton[] skeletonData = new Skeleton[temp.SkeletonArrayLength];
                temp.CopySkeletonDataTo(skeletonData);
                SkelPoints.Text = "";
                foreach (Skeleton skeleton in skeletonData)
                {
                    if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        foreach (Joint joint in skeleton.Joints)
                        {
                            SkelPoints.Text += joint.JointType.ToString() + "\t";
                            SkelPoints.Text += joint.Position.X + "\t";
                            SkelPoints.Text += joint.Position.Y + "\t";
                            SkelPoints.Text += joint.Position.Z + "\n";

                        }
                    }
                }
            }
        }
    }
}

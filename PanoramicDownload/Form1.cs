﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PanoramicDownload
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string Matchs(string str)
        {
            string txt = str;
            string re1 = "([a-z])"; // Any Single Word Character (Not Whitespace) 1
            string re2 = "(.)"; // Any Single Character 1
            string re3 = "([a-z])"; // Any Single Word Character (Not Whitespace) 2
            string re4 = "(\\d)";   // Any Single Digit 1
            string re5 = "(\\/)";   // Any Single Character 2
            string re6 = "(\\d+)";  // Integer Number 1
            string re7 = "(\\/)";   // Any Single Character 3
            string re8 = "([a-z])"; // Any Single Word Character (Not Whitespace) 3
            string re9 = "(\\d+)";  // Integer Number 2
            string re10 = "(.)";    // Any Single Character 4
            string re11 = "([a-z])";    // Any Single Word Character (Not Whitespace) 4
            string re12 = "(.)";    // Any Single Character 5
            string re13 = "(\\d+)"; // Integer Number 3
            string re14 = "(.)";    // Any Single Character 6
            string re15 = "(\\d+)"; // Integer Number 4
            string re16 = "(\\.)";  // Any Single Character 7
            string re17 = "((?:[a-z][a-z]+))";  // Word 1

            Regex r = new Regex(re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8 + re9 + re10 + re11 + re12 + re13 + re14 + re15 + re16 + re17, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(txt);
            if (m.Success)
            {
                String w1 = m.Groups[1].ToString();
                String c1 = m.Groups[2].ToString();
                String w2 = m.Groups[3].ToString();
                String d1 = m.Groups[4].ToString();
                String c2 = m.Groups[5].ToString();
                String int1 = m.Groups[6].ToString();
                String c3 = m.Groups[7].ToString();
                String w3 = m.Groups[8].ToString();
                String int2 = m.Groups[9].ToString();
                String c4 = m.Groups[10].ToString();
                String w4 = m.Groups[11].ToString();
                String c5 = m.Groups[12].ToString();
                String int3 = m.Groups[13].ToString();
                String c6 = m.Groups[14].ToString();
                String int4 = m.Groups[15].ToString();
                String c7 = m.Groups[16].ToString();
                String word1 = m.Groups[17].ToString();
                //MessageBox.Show(w1.ToString() + "" + "" + c1.ToString() + "" + "" + w2.ToString() + "" + "" + d1.ToString() + "" + "" + c2.ToString() + "" + "" + int1.ToString() + "" + "" + c3.ToString() + "" + "" + w3.ToString() + "" + "" + int2.ToString() + "" + "" + c4.ToString() + "" + "" + w4.ToString() + "" + "" + c5.ToString() + "" + "" + int3.ToString() + "" + "" + c6.ToString() + "" + "" + int4.ToString() + "" + "" + c7.ToString() + "" + "" + word1.ToString() + "" + "\n");
                return w1.ToString() + "" + "" + c1.ToString() + "" + "" + w2.ToString() + "" + "" + d1.ToString() + "" + "" + c2.ToString() + "" + "" + int1.ToString() + "" + "" + c3.ToString() + "" + "" + w3.ToString() + "" + "" + int2.ToString() + "" + "" + c4.ToString() + "" + "" + w4.ToString() + "" + "" + c5.ToString() + "" + "" + int3.ToString() + "" + "" + c6.ToString() + "" + "" + int4.ToString() + "" + "" + c7.ToString() + "" + "" + word1.ToString() + "" + "\n";

            }
            return "";
        }

        public int ImageType;
        public int ImageCount;
        public string newurltou;

        private void LoadButton_Click(object sender, EventArgs e)
        {
            jiance();
            string fileName = " ";

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = Application.StartupPath;
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                // DirectoryInfo theFolder = new DirectoryInfo(foldPath);
                //FileInfo[] dirInfo = theFolder.GetFiles();
                fileName = foldPath;
                textBox2.Text = fileName;
            }
            List<string> Down = new List<string>();

            fullfile(fileName, newurltou, ImageType.ToString(), ImageCount, DirectionType.d, Down);
            fullfile(fileName, newurltou, ImageType.ToString(), ImageCount, DirectionType.b, Down);
            fullfile(fileName, newurltou, ImageType.ToString(), ImageCount, DirectionType.f, Down);
            fullfile(fileName, newurltou, ImageType.ToString(), ImageCount, DirectionType.u, Down);
            fullfile(fileName, newurltou, ImageType.ToString(), ImageCount, DirectionType.l, Down);
            fullfile(fileName, newurltou, ImageType.ToString(), ImageCount, DirectionType.r, Down);

            MessageBox.Show("下载完成");
        }

        public void jiance()
        {
            string urlALl = textBox5.Text;
            string key = Matchs(urlALl);
            if (!isPing(textBox5.Text))
            {
                MessageBox.Show("请输入正确的链接" + isPing(urlALl), "错误提示");
                return;
            }
            if (key.Equals(""))
            {
                MessageBox.Show("请输入标准格式的全景图下载地址", "错误提示");
                return;
            }
        

            string newUrl = urlALl.Substring(0, urlALl.Length - key.Length + 1);
            newurltou = newUrl;
            List<string> newKeystr = new List<string>();
            newKeystr = GetRegex(key);
            string newkey1 = "";
            int maxtpye = 0;
            int maxIndex = 0;
            List<string> newKeystr1 = new List<string>();
            if (newKeystr[2].Contains("0"))
            {

                for (int j = 1; j < 20; j++)
                {
                    newkey1 = key.Replace(newKeystr[0], "l" + j).Replace("/" + newKeystr[1], "/01").Replace("_" + newKeystr[2] + "_", "_01_").Replace("_" + newKeystr[3] + ".", "_01.");
                    if (isPing(newUrl + newkey1))
                    {
                        maxtpye = j;
                    }
                    else
                    {
                        break;
                    }
                }
                ImageType = maxtpye;
                //MessageBox.Show(maxtpye.ToString());
                newkey1 = key.Replace(newKeystr[0], "l" + maxtpye).Replace("/" + newKeystr[1], "/01").Replace("_" + newKeystr[2] + "_", "_01_").Replace("_" + newKeystr[3] + ".", "_01.");

                newKeystr1 = GetRegex(newkey1);
                for (int i = 1; i < 20; i++)
                {
                    string value1 = "";
                    if (i >= 10)
                    {
                        value1 = newkey1.Replace("/" + newKeystr1[1] + "/", "/" + i + "/").Replace("_" + newKeystr1[2] + "_", "_" + i + "_");
                        if (isPing(newUrl + value1))
                        {
                            maxIndex = i;
                        }
                        else
                        {
                            break;
                        }
                    }
                    value1 = newkey1.Replace("/" + newKeystr1[1] + "/", "/0" + i + "/").Replace("_" + newKeystr1[2] + "_", "_0" + i + "_");
                    if (isPing(newUrl + value1))
                    {
                        maxIndex = i;
                    }
                    else
                    {
                        break;
                    }
                }
                ImageCount = maxIndex;
                //MessageBox.Show(maxIndex.ToString());
                return;
            }

            for (int j = 1; j < 20; j++)
            {
                newkey1 = key.Replace(newKeystr[0], "l" + j).Replace("/" + newKeystr[1], "/1").Replace("_" + newKeystr[2] + "_", "_1_").Replace("_" + newKeystr[3] + ".", "_1.");
                if (isPing(newUrl + newkey1))
                {
                    maxtpye = j;
                }
                else
                {
                    break;
                }
            }
            ImageType = maxtpye;
            //MessageBox.Show(maxtpye.ToString());
            newkey1 = key.Replace(newKeystr[0], "l" + maxtpye).Replace("/" + newKeystr[1], "/1").Replace("_" + newKeystr[2] + "_", "_1_").Replace("_" + newKeystr[3] + ".", "_1.");
            newKeystr1 = GetRegex(newkey1);
            for (int i = 1; i < 20; i++)
            {
                string value1 = newkey1.Replace("/" + newKeystr1[1] + "/", "/" + i + "/").Replace("_" + newKeystr1[2] + "_", "_" + i + "_");
                if (isPing(newUrl + value1))
                {
                    maxIndex = i;
                }
                else
                {
                    break;
                }
            }
            //MessageBox.Show(maxIndex.ToString());
            ImageCount = maxIndex;
        }


        public bool isPing(string url)
        {
            if (GetWebStatusCode(url, 1000).Equals("200"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<String> GetRegex(string txt)
        {

            //string txt = "";

            string re1 = ".*?"; // Non-greedy match on filler
            string re2 = "[a-z]";   // Uninteresting: w
            string re3 = ".*?"; // Non-greedy match on filler
            string re4 = "([a-z])"; // Any Single Word Character (Not Whitespace) 1
            string re5 = "(\\d)";   // Any Single Digit 1
            string re6 = ".*?"; // Non-greedy match on filler
            string re7 = "(\\d+)";  // Integer Number 1
            string re8 = ".*?"; // Non-greedy match on filler
            string re9 = "\\d+";    // Uninteresting: int
            string re10 = ".*?";    // Non-greedy match on filler
            string re11 = "(\\d+)"; // Integer Number 2
            string re12 = ".*?";    // Non-greedy match on filler
            string re13 = "(\\d+)"; // Integer Number 3
            List<string> keyStr = new List<string>();
            Regex r = new Regex(re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8 + re9 + re10 + re11 + re12 + re13, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(txt);
            if (m.Success)
            {
                String w1 = m.Groups[1].ToString();
                String d1 = m.Groups[2].ToString();
                String int1 = m.Groups[3].ToString();
                String int2 = m.Groups[4].ToString();
                String int3 = m.Groups[5].ToString();

                keyStr.Add(w1 + d1);
                keyStr.Add(int1);
                keyStr.Add(int2);
                keyStr.Add(int3);
                return keyStr;
                // MessageBox.Show("(" + w1.ToString() + ")" + "(" + d1.ToString() + ")" + "(" + int1.ToString() + ")" + "(" + int2.ToString() + ")" + "(" + int3.ToString() + ")" + "\n");
                //Console.Write("(" + w1.ToString() + ")" + "(" + d1.ToString() + ")" + "(" + int1.ToString() + ")" + "(" + int2.ToString() + ")" + "(" + int3.ToString() + ")" + "\n");
            }
            return null;
        }

        public void fullfile(string confi, string Url, string index, int forint, DirectionType mode, List<string> sw)
        {
            for (int i = 1; i <= forint; i++)
            {
                for (int x = 1; x <= forint; x++)
                {
                    bool get = isPing(Url + mode + "/" + "l" + index + "/" + i + "/l" + index + "_" + mode + "_" + i + "_" + x + ".jpg");
                    if (get)
                    {
                        string url = "";
                        var command = " -j 5  " + Url + mode + "/l" + index + "/" + i + "/l" + index + "_" + mode + "_" + i + "_" + x + ".jpg  -d "+ confi+ "";
                        using (var p = new Process())
                        {
                            RedirectExcuteProcess(p, @"D:\test\aria2c.exe", command, (s, e) => ShowInfo(url, e.Data));
                        }
                    }
                }
            }
           
        }

        private void ShowInfo(string url, string a)
        {
            if (a == null) return;

            Thread_proc(a);
        }
        /// <summary>
        /// 重定向
        /// </summary>
        /// <param name="p"></param>
        /// <param name="exe"></param>
        /// <param name="arg"></param>
        /// <param name="output"></param>
        private void RedirectExcuteProcess(Process p, string exe, string arg, DataReceivedEventHandler output)
        {
            p.StartInfo.FileName = exe;
            p.StartInfo.Arguments = arg;

            p.StartInfo.UseShellExecute = false;    //输出信息重定向
            p.StartInfo.ErrorDialog = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardOutput = true;


            p.OutputDataReceived += output;
            p.ErrorDataReceived += output;


            p.Start();                    //启动线程
            Application.DoEvents();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            //p.WaitForExit();            //等待进程结束
        }


        public void getimg(string filepath, string imgName, string tpye, int index)
        {

            ListViewItem lvi = new ListViewItem();
            //progressBar1

            ProgressBar dd = new ProgressBar();

            //dd.Maximum = 169;
            this.listView1.BeginUpdate();
            lvi.Text = tpye + ".jpg";
            int idd = 0;
            lvi.SubItems.Add("");
            lvi.SubItems.Add("");

            this.listView1.Items.Add(lvi);

            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
            int contwidth = 0;
            for (int x = 1; x <= index; x++)
            {
                Image image3 = Image.FromFile(filepath + "l" + imgName + "_" + tpye + "_" + 1 + "_" + x + ".jpg");
                int i = image3.Width;
                contwidth += i;
                image3.Dispose();
            }

            Image bmp = new Bitmap(contwidth, contwidth, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(bmp);
            int high = 0;
            for (int i = 1; i <= index; i++)
            {
                int width = 0;
                for (int d = 1; d <= index; d++)
                {
                    Application.DoEvents();
                    Image image1 = Image.FromFile(filepath + "l" + imgName + "_" + tpye + "_" + i + "_" + d + ".jpg");
                    g.DrawImage(image1, width, high, image1.Width, image1.Height);
                    width += image1.Width;
                    image1.Dispose();
                    idd++;

                    this.listView1.BeginUpdate();
                    //lvi.SubItems[2].Text = idd.ToString();
                    dd.Parent = listView1;
                    dd.SetBounds(lvi.SubItems[1].Bounds.X, lvi.SubItems[1].Bounds.Y, lvi.SubItems[1].Bounds.Width, lvi.SubItems[1].Bounds.Height);


                    System.Threading.Thread.Sleep(5);
                    dd.Value = (int)(idd / (ImageCount*ImageCount));

                    this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
                    Application.DoEvents();
                    lvi.SubItems[2].Text = (int)(idd / (ImageCount * ImageCount)) + "%";
                    if (dd.Value == 100)
                    {
                        lvi.SubItems[2].Text = "合成完成";
                    }
                }
                Image image2 = Image.FromFile(filepath + "l" + imgName + "_" + tpye + "_" + i + "_" + i + ".jpg");
                high += image2.Height;
                image2.Dispose();
            }

            g.Flush();
            g.Dispose();
            bmp.Save(AppDomain.CurrentDomain.BaseDirectory + "下载文件/" + tpye + ".JPG", ImageFormat.Jpeg);
            bmp.Dispose();
            //ImagePath.Add(tpye, AppDomain.CurrentDomain.BaseDirectory + "下载文件\\" + tpye + ".JPG");

        }

        delegate void d(string args);
        private delegate void UpdateInfo(string str);
        void Thread_proc(string args)
        {
            if (this.InvokeRequired)
            {
                d dd = new d(Thread_proc);
                this.Invoke(dd, new object[] { args });
            }
            else
            {
                if (args == null)
                {
                    return;
                }

                const string re1 = ".*?"; // Non-greedy match on filler
                const string re2 = "(\\(.*\\))"; // Round Braces 1

                var r = new Regex(re1 + re2, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                var m = r.Match(args);
                if (m.Success)
                {
                    var rbraces1 = m.Groups[1].ToString().Replace("(", "").Replace(")", "").Replace("%", "").Replace("s", "0");
                    if (rbraces1 == "OK")
                    {
                        rbraces1 = "100";
                    }
                }
            }
        }

        delegate void imags(string args, string args1, string args2, int index);
        void Thread1_proc(string args, string args1, string args2, int index)
        {
            if (this.InvokeRequired)
            {
                imags dd = new imags(Thread1_proc);
                this.Invoke(dd, new object[] { args, args1, args2, index });
            }
            else
            {
                if (args == null)
                {
                    return;
                }
                getimg(args, args1, args2, index);
            }
        }


        #region 一般级
        /// <summary>
        /// 配置文件路径按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = OpenText();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto:yzj520mei@126.com,test2@sample.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("chrome.exe", "http://wpa.qq.com/msgrd?v=3&uin=1228267639&site=qq&menu=yes");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (toolTip1.GetToolTip(pictureBox1).Equals("你瞅啥？？"))
            {
                this.toolTip1.SetToolTip(pictureBox1, "瞅你咋的！！");
            }
            else
            {
                this.toolTip1.SetToolTip(pictureBox1, "你瞅啥？？");
            }
        }
        /// <summary>
        /// 打开路径
        /// </summary>
        /// <returns></returns>
        public string OpenText()
        {
            using (OpenFileDialog OpenFD = new OpenFileDialog())     //实例化一个 OpenFileDialog 的对象
            {
                //定义打开的默认文件夹位置
                OpenFD.InitialDirectory = Application.StartupPath;
                OpenFD.Filter = "All files(*.*)|*.*|txt files(*.txt)|*.txt";
                OpenFD.FilterIndex = 2;
                OpenFD.ShowDialog();                  //显示打开本地文件的窗体
                OpenFD.RestoreDirectory = true;
                return OpenFD.FileName;       //把 路径名称 赋给 fileName
            }
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            //string str = Matchs(textBox5.Text);
            // MessageBox.Show(GetWebStatusCode(textBox5.Text, 100));
            //listView1.ResetText();

        }

        /// <summary>
        /// 判断链接 200
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string GetWebStatusCode(string url, int timeout)
        {
            HttpWebRequest req = null;
            try
            {
                req = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
                req.Method = "HEAD";  //这是关键        
                req.Timeout = timeout;
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                return Convert.ToInt32(res.StatusCode).ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (req != null)
                {
                    req.Abort();
                    req = null;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"下载文件\";
            if (!ImageType.Equals(0) || !ImageCount.Equals(0))
            {
                getimg(path, ImageType.ToString(), "d", ImageCount);//2304//4608//3072
                getimg(path, ImageType.ToString(), "f", ImageCount);//2304//4608//3072
                getimg(path, ImageType.ToString(), "b", ImageCount);//2304//4608//3072
                getimg(path, ImageType.ToString(), "u", ImageCount);//2304//4608//3072
                getimg(path, ImageType.ToString(), "l", ImageCount);//2304//4608//3072
                getimg(path, ImageType.ToString(), "r", ImageCount);//2304//4608//3072
            }

            
           
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

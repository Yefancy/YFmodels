using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace YFmodels
{
    public class LparseUtils
    {
        public static YFProgram GetProgram(string lparse)
        {
            byte[] data = System.Text.Encoding.Default.GetBytes(lparse);
            var IO = File.Create("lparse_tmp");
            IO.Write(data, 0, data.Length);
            IO.Close();
            var k = ExcuteCmd("lparse lparse_tmp");
            List<string> striparr = k.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            var pro = new YFProgram();
            int i = 4;
            //rule
            try
            {
                for (; i < striparr.Count; i++)
                {
                    string ruleSS = striparr[i];
                    if (ruleSS[0] == '0') break;
                    List<string> ruleS = ruleSS.Split(' ').ToList();
                    if (int.Parse(ruleS[0]) == 1)
                    {
                        Rule rule = new Rule();
                        Atom head = null;
                        int headname = int.Parse(ruleS[1]);
                        foreach (var a in pro.atoms)
                        {
                            if (a.atom == headname)
                                head = a;
                        }
                        if (head == null)
                        {
                            head = new Atom(headname);
                            pro.atoms.Add(head);
                        }
                        head.hList.Add(rule);
                        rule.head = head;

                        int literals = int.Parse(ruleS[2]);
                        int notliterals = int.Parse(ruleS[3]);
                        for (int kk = 4; kk < ruleS.Count - 2; kk++)
                        {
                            if (ruleS[kk] == "") continue;
                            Atom body = null;
                            int bodyname = int.Parse(ruleS[kk]);
                            foreach (var a in pro.atoms)
                            {
                                if (a.atom == bodyname)
                                    body = a;
                            }
                            if (body == null)
                            {
                                body = new Atom(bodyname);
                                pro.atoms.Add(body);
                            }
                            if (notliterals != 0)
                            {
                                body.nList.Add(rule);
                                rule.nBody.Add(body);
                                notliterals--;
                            }
                            else
                            {
                                body.pList.Add(rule);
                                rule.pBody.Add(body);
                            }
                        }
                        pro.rules.Add(rule);
                    }
                }
                //dic
                i++;
                for (; i < striparr.Count; i++)
                {
                    string ruleSS = striparr[i];
                    if (ruleSS[0] == '0') break;
                    List<string> ruleS = ruleSS.Split(' ').ToList();
                    int name = int.Parse(ruleS[0]);
                    string Key = ruleS[1];
                    pro.dic.Add(name, Key);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("非法lparse语法");
            }
            
            return pro;
        }

        public static string ExcuteCmd(string para)
        {
            Process p = new Process();
            p.StartInfo.CreateNoWindow = true;         // 不创建新窗口    
            p.StartInfo.UseShellExecute = false;       //不启用shell启动进程  
            p.StartInfo.RedirectStandardInput = true;  // 重定向输入    
            p.StartInfo.RedirectStandardOutput = true; // 重定向标准输出    
            p.StartInfo.RedirectStandardError = true;  // 重定向错误输出  
            p.StartInfo.FileName = "cmd.exe";
            p.Start();

            p.StandardInput.WriteLine(para + "&exit");
            p.StandardInput.AutoFlush = true;
            p.StandardInput.Close();

            string output = p.StandardOutput.ReadToEnd();
            //  p.OutputDataReceived += new DataReceivedEventHandler(processOutputDataReceived);
            p.WaitForExit();//参数单位毫秒，在指定时间内没有执行完则强制结束，不填写则无限等待
            p.Close();
            return output;
        }
    }
}

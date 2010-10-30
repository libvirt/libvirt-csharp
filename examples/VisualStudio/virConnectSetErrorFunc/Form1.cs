/*
 * Copyright (C)
 *   Arnaud Champion <arnaud.champion@devatom.fr>
 *   Jaromír Červenka <cervajz@cervajz.com>
 *
 * See COPYING.LIB for the License of this software
 *
 * Sample code for :
 * Functions :
 *      Connect.OpenAuth
 *      Domain.GetInfo
 *      Domain.LookupByName
 *      Errors.GetLastError
 *      Errors.SetErrorFunc
 *
 * Types :
 *      DomainInfo
 *      Error
 *
 */

using System;
using System.Windows.Forms;
using Libvirt;

namespace virConnectSetErrorFunc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            IntPtr conn = Connect.Open(tbURI.Text);

            if (conn != IntPtr.Zero)
            {
                Errors.SetErrorFunc(IntPtr.Zero, ErrorCallback);
                IntPtr domain = Domain.LookupByName(conn, tbDomainName.Text);
                DomainInfo di = new DomainInfo();
                Domain.GetInfo(domain, di);
                textBox1.Text = di.State.ToString();
                textBox2.Text = di.maxMem.ToString();
                textBox3.Text = di.memory.ToString();
                textBox4.Text = di.nrVirtCpu.ToString();
                textBox5.Text = di.cpuTime.ToString();
            }

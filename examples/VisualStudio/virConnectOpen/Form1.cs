/*
 * Copyright (C)
 *   Arnaud Champion <arnaud.champion@devatom.fr>
 *   Jaromír Červenka <cervajz@cervajz.com>
 *
 * See COPYING.LIB for the License of this software
 * 
 * Sample code for :
 * Function :
 *      virConnectOpen
 *      virConnectNumOfStoragePools
 *      virConnectListStoragePools
 */

using System;
using System.Windows.Forms;
using Libvirt;

namespace virConnectOpen
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
                int numOfStoragePools = Connect.NumOfStoragePools(conn);
                if (numOfStoragePools == -1)
                    ShowError("Unable to get the number of storage pools");
                string[] storagePoolsNames = new string[numOfStoragePools];
                int listStoragePools = Connect.ListStoragePools(conn, ref storagePoolsNames, numOfStoragePools);
                if (listStoragePools == -1)
                    ShowError("Unable to list storage pools");
                foreach (string storagePoolName in storagePoolsNames)
                    lbStoragePool.Items.Add(storagePoolName);
            }
            else
            {
                ShowError("Unable to connect");
            }
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

/*
 * Copyright (C)
 *   Arnaud Champion <arnaud.champion@devatom.fr>
 *   Jaromír Červenka <cervajz@cervajz.com>
 *
 * See COPYING.LIB for the License of this software
 *
 * Sample code for :
 * Function :
 *      Connect.Open
 *      Connect.NumOfDomains
 *      Connect.ListDomains
 *      Domain.BlockStats
 *      Domain.GetInfo
 *      Domain.GetName
 *      Domain.GetXMLDesc
 *      Domain.InterfaceStats
 *      Domain.ListDomains
 *      Domain.LookupByID
 *      Domain.LookupByName
 *      Errors.SetErrorFunc
 *
 * Types :
 *      DomainBlockStatsStruct
 *      DomainInterfaceStatsStruct
 *      DomainInfo
 *      Error
 *
 */

using System;
using Gtk;
using Libvirt;
using System.Collections;
using System.Xml;
using System.Linq;

public partial class MainWindow : Gtk.Window
{
	private IntPtr _conn = IntPtr.Zero;
    private IntPtr _domainPtr = IntPtr.Zero;
	private ListStore _domainsStore;
	private ListStore _blockDevStore;
	private ListStore _interfaceStore;

	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();

		_domainsStore = new ListStore(typeof(string));
		CellRendererText cell = new CellRendererText();
		cbDomains.PackStart(cell, false);
		cbDomains.AddAttribute(cell, "text", 0);
		cbDomains.Model = _domainsStore;

		_blockDevStore = new ListStore(typeof(string));
		cbBlockDevice.PackStart(cell, false);
		cbBlockDevice.AddAttribute(cell, "text", 0);
		cbBlockDevice.Model = _blockDevStore;

		_interfaceStore = new ListStore(typeof(string));
		cbInterface.PackStart(cell, false);
		cbInterface.AddAttribute(cell, "text", 0);
		cbInterface.Model = _interfaceStore;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Domain.Free(_domainPtr);
		Connect.Close(_conn);
		Application.Quit ();
		a.RetVal = true;
	}

	protected virtual void OnButton1Clicked (object sender, System.EventArgs e)
	{
		_conn = Connect.Open(entry1.Text);
        Errors.SetErrorFunc(IntPtr.Zero, ErrorCallback);
		_domainsStore.Clear();
        int nbDomains = Connect.NumOfDomains(_conn);
        int[] domainIDs = new int[nbDomains];
        Connect.ListDomains(_conn, domainIDs, nbDomains);
		_domainsStore.Clear();
        foreach (int domainID in domainIDs)
        {
			IntPtr domainPtr = Domain.LookupByID(_conn, domainID);
			string domainName = Domain.GetName(domainPtr);
			_domainsStore.AppendValues(domainName);
            Domain.Free(domainPtr);
        }
		TreeIter iter;
		_domainsStore.GetIterFirst(out iter);
		cbDomains.SetActiveIter(iter);
	}

	private void ErrorCallback(IntPtr userData, Error error)
    {
        ShowError(error);
    }

	private void ShowError(Error error)
	{
		MessageDialog errorMsg = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, error.Message);
        errorMsg.Title = "Error";
		if ((ResponseType) errorMsg.Run() == ResponseType.Close)
		{
            errorMsg.Destroy();
		}
	}

	protected virtual void OnCbDomainsChanged (object sender, System.EventArgs e)
	{
		TreeIter currentIter;
		cbDomains.GetActiveIter(out currentIter);
		string selectedDomainName =  (string) _domainsStore.GetValue(currentIter, 0);

		_domainPtr = Domain.LookupByName(_conn, selectedDomainName);

        UpdateDomainInfo();

		_blockDevStore.Clear();
		foreach (string dev in  GetDomainBlockDevices(Domain.GetXMLDesc(_domainPtr, 0)))
        {
            _blockDevStore.AppendValues(dev);
        }
		TreeIter iter;
		_blockDevStore.GetIterFirst(out iter);
		cbBlockDevice.SetActiveIter(iter);

        _interfaceStore.Clear();
        foreach (string iface in GetDomainInterfaces(Domain.GetXMLDesc(_domainPtr, 0)))
        {
            _interfaceStore.AppendValues(iface);
        }
        _interfaceStore.GetIterFirst(out iter);
		cbInterface.SetActiveIter(iter);
	}

	private void UpdateDomainInfo()
    {
        DomainInfo domainInfo = new DomainInfo();
        Domain.GetInfo(_domainPtr, domainInfo);

        eState.Text = domainInfo.State.ToString();
        eMaxMem.Text = domainInfo.maxMem.ToString();
        eMemory.Text = domainInfo.memory.ToString();
        eNumVirtCPU.Text = domainInfo.nrVirtCpu.ToString();
        eCPUTime.Text = domainInfo.cpuTime.ToString();
    }

	private string[] GetDomainBlockDevices(string xmlDomainDescription)
    {
        XmlDocument xmlDescription = new XmlDocument();
        xmlDescription.LoadXml(xmlDomainDescription);
        XmlNodeList devNodeList = xmlDescription.SelectNodes("//domain/devices/disk");

        return (from XmlNode xn in devNodeList select xn.SelectSingleNode("target").Attributes["dev"].Value).ToArray();
    }

	private string[] GetDomainInterfaces(string xmlDomainDescription)
    {
        XmlDocument xmlDescription = new XmlDocument();
        xmlDescription.LoadXml(xmlDomainDescription);
        XmlNodeList iFaceNodeList = xmlDescription.SelectNodes("//domain/devices/interface");

        return (from XmlNode xn in iFaceNodeList select xn.SelectSingleNode("target").Attributes["dev"].Value).ToArray();
    }

	protected virtual void OnCbBlockDeviceChanged (object sender, System.EventArgs e)
	{
		TreeIter currentIter;
		cbBlockDevice.GetActiveIter(out currentIter);
		string selectedBlockDev =  (string) _blockDevStore.GetValue(currentIter, 0);
		if (!string.IsNullOrEmpty(selectedBlockDev))
		{
			DomainBlockStatsStruct blockStat;
	        Domain.BlockStats(_domainPtr, selectedBlockDev, out blockStat);
	        eReadRequests.Text = blockStat.rd_req.ToString();
	        eReadBytes.Text = blockStat.rd_bytes.ToString();
	        eWriteRequests.Text = blockStat.wr_req.ToString();
	        eWriteBytes.Text = blockStat.wr_bytes.ToString();
	        eErrors.Text = blockStat.errs.ToString();
		}
	}

	protected virtual void OnCbInterfaceChanged (object sender, System.EventArgs e)
	{
		TreeIter currentIter;
		cbInterface.GetActiveIter(out currentIter);
		string selectedInterface =  (string) _interfaceStore.GetValue(currentIter, 0);
		if (!string.IsNullOrEmpty(selectedInterface))
		{
			DomainInterfaceStatsStruct interfaceStat;
	        Domain.InterfaceStats(_domainPtr, selectedInterface, out interfaceStat);
	        eRxBytes.Text = interfaceStat.rx_bytes.ToString();
	        eRxDrops.Text = interfaceStat.rx_drop.ToString();
	        eRxErrs.Text = interfaceStat.rx_errs.ToString();
	        eRxPackets.Text = interfaceStat.rx_packets.ToString();
	        eTxBytes.Text = interfaceStat.tx_bytes.ToString();
	        eTxDrops.Text = interfaceStat.tx_drop.ToString();
	        eTxErrs.Text = interfaceStat.tx_errs.ToString();
	        eTxPackets.Text = interfaceStat.tx_packets.ToString();
		}
	}



}

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Vestris.VMWareLib;
using AppUtil;

namespace VirtualLib
{
    class CommonInfo
    {
        public string getProductId(string template)
        {
        }
    }
    class VMHost
    {
        //list of templates available on this host can be incorporated here

        static VimService _service;       
        private AppUtil.AppUtil cb = null;        
        private ServiceContent _sic;
        string[] connectString;

        private VMWareVirtualHost handle;

        public VMHost(string url, string server, string portNumber, string userName, string password)
        {            
            string tempConnectString;

            tempConnectString = " --url " + url + " --server " + server + " --portnumber " + portnumber + " --username " + username + " --password " + password + " --ignorecert";
            connectString = tempConnectString.Trim().Split(new char[] { ' ' });
        }

        // use templateExists in deploy to check for existence of guest.templateName
        public bool templateExists(VM guest)
        {
        }

        public VMWareVirtualHost getHandle()
        {
            return handle;
        }

        public void deploy(VM guest)
        {
            cb = AppUtil.AppUtil.initialize("VMDeploy", this.connectString);
            cb.connect();
            /************Start Deploy Code*****************/

            /************End Deploy Code*******************/
            cb.disConnect();
            
        }
    } 

    
    abstract class VM
    {
        
        protected CommonInfo cInfo;

        //If systemName is given as a parameter then use that otherwise autogenerate
        protected VMHost hostRef = null;
        protected string templateName;
        protected string datacenterName;
        protected string[] dnsList;
        protected string workGroupPassword;
        protected string domainAdmin;
        protected string domainPassword;
        protected string joinDomain;
        protected string name; //Machine name
        protected string productId;
        protected int rebootCount;
        protected string cloneName;
        protected string vmPath;        

        public VM(VMHost hostRef)
        {
            this.hostRef = hostRef;
        }

        public string getName()
        {
            return this.name;
        }

        public string getProductId()
        {
            return this.productId;
        }

        public string[] getDnsList()
        {
            return this.dnsList;
        }

        public string getWorkGroupPassword()
        {
            return this.workGroupPassword;
        }

        public string getDomainAdmin()
        {
            return this.domainAdmin;
        }

        public string getDomainPassword()
        {
            return this.domainPassword;
        }

        public string getJoinDomain()
        {
            return this.joinDomain;
        }

        public string getProductId()
        {
            return this.productId;
        }

        public string getRebootCount()
        {
            return this.rebootCount;
        }

        public string getCloneName()
        {
            return this.cloneName;
        }

        public string getVmPath()
        {
            return this.vmPath;
        }

        public void defineSysprepParameters(string templateName, string systemName, string[] dnsList, string workGroupPassword, string domainAdmin, string domainPassword, string joinDomain, strint productId)
        {
            this.templateName = templateName;

            if (systemName != null)
            {
                this.name = systemName;
                this.cloneName = systemName;
            }
            else
            {
                this.name = this.cloneName = DateTime.Now.Second + DateTime.Now.Millisecond;
            }
            this.dnsList = dnsList;
            this.workGroupPassword = workGroupPassword;
            this.domainAdmin = domainAdmin;
            this.domainPassword = domainPassword;
            this.joinDomain = joinDomain;
            this.productId = cInfo.getProductId(this.templateName);            
            this.vmPath = "/" + this.datacenterName + "/vm/" + this.templateName;
        }
    }

    public class Win2003VM : VM
    {
        public Win2003VM(VMHost hostRef)
            : base(hostRef)
        {
            this.rebootCount = 2;
        }      
    }

    public class Win2008VM : VM
    {
        public Win2008VM(VMHost hostRef)
            : base(hostRef)
        {
            this.rebootCount = 1;
        }
    }    
}
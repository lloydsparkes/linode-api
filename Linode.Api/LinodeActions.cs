using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linode.Api
{
    internal class LinodeActions
    {
        public const string LINODE_BOOT = "linode.boot";
        public const string LINODE_CLONE = "linode.clone";
        public const string LINODE_CREATE = "linode.create";
        public const string LINODE_DELETE = "linode.delete";
        public const string LINODE_LIST = "linode.list";
        public const string LINODE_REBOOT = "linode.reboot";
        public const string LINODE_RESIZE = "linode.resize";
        public const string LINODE_SHUTDOWN = "linode.shutdown";
        public const string LINODE_UPDATE = "linode.update";

        public const string LINODE_CONFIG_CREATE = "linode.config.create";
        public const string LINODE_CONFIG_DELETE = "linode.config.delete";
        public const string LINODE_CONFIG_LIST = "linode.config.list";
        public const string LINODE_CONFIG_UPDATE = "linode.config.update";

        public const string LINODE_DISK_CREATE = "linode.disk.create";
        public const string LINODE_DISK_CREATEFROMDISTRIBUTION = "linode.disk.createfromdistribution";
        public const string LINODE_DISK_CREATEFROMSTACKSCRIPT = "linode.disk.createfromstackscript";
        public const string LINODE_DISK_DELETE = "linode.disk.delete";
        public const string LINODE_DISK_DUPLICATE = "linode.disk.duplicate";
        public const string LINODE_DISK_LIST = "linode.disk.list";
        public const string LINODE_DISK_RESIZE = "linode.disk.resize";
        public const string LINODE_DISK_UPDATE = "linode.disk.update";

        public const string LINODE_IP_ADDPRIVATE = "linode.ip.addprivate";
        public const string LINODE_IP_LIST = "linode.ip.list";

        public const string LINODE_JOB_LIST = "linode.job.list";

        public const string NODEBALANCER_CREATE = "nodebalancer.create";
        public const string NODEBALANCER_DELETE = "nodebalancer.delete";
        public const string NODEBALANCER_LIST = "nodebalancer.list";
        public const string NODEBALANCER_UPDATE = "nodebalancer.update";

        public const string NODEBALANCER_CONFIG_CREATE = "nodebalancer.config.create";
        public const string NODEBALANCER_CONFIG_DELETE = "nodebalancer.config.delete";
        public const string NODEBALANCER_CONFIG_LIST = "nodebalancer.config.list";
        public const string NODEBALANCER_CONFIG_UPDATE = "nodebalancer.config.update";

        public const string NODEBALANCER_NODE_CREATE = "nodebalancer.node.create";
        public const string NODEBALANCER_NODE_DELETE = "nodebalancer.node.delete";
        public const string NODEBALANCER_NODE_LIST = "nodebalancer.node.list";
        public const string NODEBALANCER_NODE_UPDATE = "nodebalancer.node.update";

        public const string STACKSCRIPT_CREATE = "stackscript.create";
        public const string STACKSCRIPT_DELETE = "stackscript.delete";
        public const string STACKSCRIPT_LIST = "stackscript.list";
        public const string STACKSCRIPT_UPDATE = "stackscript.update";

        public const string DOMAIN_CREATE = "domain.create";
        public const string DOMAIN_DELETE = "domain.delete";
        public const string DOMAIN_LIST = "domain.list";
        public const string DOMAIN_UPDATE = "domain.update";

        public const string DOMAIN_RESOURCE_CREATE = "domain.resource.create";
        public const string DOMAIN_RESOURCE_DELETE = "domain.resource.delete";
        public const string DOMAIN_RESOURCE_LIST = "domain.resource.list";
        public const string DOMAIN_RESOURCE_UPDATE = "domain.resource.update";

        public const string API_SPEC = "api.spec";
        public const string USER_GETAPIKEY = "user.getapikey";
        public const string TEST_ECHO = "test.echo";
        public const string ACCOUNT_INFO = "account.info";

        public const string LINODEPLANS = "avail.linodeplans";
        public const string DATACENTERS = "avail.datacenters";
        public const string DISTRIBUTIONS = "avail.distributions";
        public const string KERNELS = "avail.kernels";
        public const string STACKSCRIPTS = "avail.stackscripts";
    }
}

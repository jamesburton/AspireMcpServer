using System.ComponentModel;
using AspireMcpServer.Core;
using ModelContextProtocol.Server;

namespace AspireMcpServer.Services.Extensions;

/// <summary>
/// Provides .NET Aspire container orchestration platform integration commands
/// </summary>
[McpServerToolType]
public static class AspireOrchestrationCommands
{
    [McpServerTool, Description("Deploys Aspire application to Kubernetes cluster")]
    public static async Task<string> DeployToKubernetesAsync(
        [Description("Kubernetes context name")] string context,
        [Description("Namespace")] string namespace_,
        [Description("Deployment name")] string deploymentName,
        [Description("Container image")] string image,
        [Description("Replica count")] int? replicas = null,
        [Description("Service type (ClusterIP,NodePort,LoadBalancer)")] string? serviceType = null,
        [Description("Service port")] int? servicePort = null,
        [Description("Resource limits (cpu=100m,memory=128Mi)")] string? resourceLimits = null,
        [Description("Environment variables (key=value,key2=value2)")] string? environmentVariables = null,
        [Description("ConfigMap name")] string? configMapName = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"k8s deploy --context \"{context}\" --namespace \"{namespace_}\" --deployment \"{deploymentName}\" --image \"{image}\"";
        
        if (replicas.HasValue)
            args += $" --replicas {replicas}";
        if (!string.IsNullOrEmpty(serviceType))
            args += $" --service-type {serviceType}";
        if (servicePort.HasValue)
            args += $" --service-port {servicePort}";
        if (!string.IsNullOrEmpty(resourceLimits))
            args += $" --resource-limits \"{resourceLimits}\"";
        if (!string.IsNullOrEmpty(environmentVariables))
            args += $" --env \"{environmentVariables}\"";
        if (!string.IsNullOrEmpty(configMapName))
            args += $" --config-map \"{configMapName}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Manages Kubernetes ConfigMaps for Aspire applications")]
    public static async Task<string> ManageKubernetesConfigMapAsync(
        [Description("Action (create,update,delete,get)")] string action,
        [Description("Kubernetes context name")] string context,
        [Description("Namespace")] string namespace_,
        [Description("ConfigMap name")] string configMapName,
        [Description("Configuration data (key=value,key2=value2)")] string? configData = null,
        [Description("Configuration file path")] string? configFile = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"k8s configmap {action} --context \"{context}\" --namespace \"{namespace_}\" \"{configMapName}\"";
        
        if (!string.IsNullOrEmpty(configData))
            args += $" --data \"{configData}\"";
        if (!string.IsNullOrEmpty(configFile))
            args += $" --from-file \"{configFile}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Manages Kubernetes Secrets for Aspire applications")]
    public static async Task<string> ManageKubernetesSecretAsync(
        [Description("Action (create,update,delete,get)")] string action,
        [Description("Kubernetes context name")] string context,
        [Description("Namespace")] string namespace_,
        [Description("Secret name")] string secretName,
        [Description("Secret type (generic,tls,docker-registry)")] string? secretType = null,
        [Description("Secret data (key=value,key2=value2)")] string? secretData = null,
        [Description("Certificate file path (for TLS secrets)")] string? certFile = null,
        [Description("Private key file path (for TLS secrets)")] string? keyFile = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"k8s secret {action} --context \"{context}\" --namespace \"{namespace_}\" \"{secretName}\"";
        
        if (!string.IsNullOrEmpty(secretType))
            args += $" --type {secretType}";
        if (!string.IsNullOrEmpty(secretData))
            args += $" --data \"{secretData}\"";
        if (!string.IsNullOrEmpty(certFile))
            args += $" --cert \"{certFile}\"";
        if (!string.IsNullOrEmpty(keyFile))
            args += $" --key \"{keyFile}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Configures Kubernetes Horizontal Pod Autoscaler for Aspire applications")]
    public static async Task<string> ConfigureKubernetesHpaAsync(
        [Description("Kubernetes context name")] string context,
        [Description("Namespace")] string namespace_,
        [Description("Deployment name")] string deploymentName,
        [Description("Minimum replicas")] int minReplicas,
        [Description("Maximum replicas")] int maxReplicas,
        [Description("CPU target percentage")] int? cpuTargetPercentage = null,
        [Description("Memory target percentage")] int? memoryTargetPercentage = null,
        [Description("Custom metrics configuration")] string? customMetrics = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"k8s hpa configure --context \"{context}\" --namespace \"{namespace_}\" --deployment \"{deploymentName}\" --min-replicas {minReplicas} --max-replicas {maxReplicas}";
        
        if (cpuTargetPercentage.HasValue)
            args += $" --cpu-target {cpuTargetPercentage}";
        if (memoryTargetPercentage.HasValue)
            args += $" --memory-target {memoryTargetPercentage}";
        if (!string.IsNullOrEmpty(customMetrics))
            args += $" --custom-metrics \"{customMetrics}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Deploys Aspire application to Docker Swarm")]
    public static async Task<string> DeployToDockerSwarmAsync(
        [Description("Service name")] string serviceName,
        [Description("Container image")] string image,
        [Description("Replica count")] int? replicas = null,
        [Description("Published port mapping (host:container)")] string? portMapping = null,
        [Description("Network name")] string? networkName = null,
        [Description("Environment variables (key=value,key2=value2)")] string? environmentVariables = null,
        [Description("Volume mounts (source:target)")] string? volumeMounts = null,
        [Description("Resource constraints (cpu=0.5,memory=512M)")] string? resourceConstraints = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"docker-swarm deploy --service \"{serviceName}\" --image \"{image}\"";
        
        if (replicas.HasValue)
            args += $" --replicas {replicas}";
        if (!string.IsNullOrEmpty(portMapping))
            args += $" --port \"{portMapping}\"";
        if (!string.IsNullOrEmpty(networkName))
            args += $" --network \"{networkName}\"";
        if (!string.IsNullOrEmpty(environmentVariables))
            args += $" --env \"{environmentVariables}\"";
        if (!string.IsNullOrEmpty(volumeMounts))
            args += $" --volume \"{volumeMounts}\"";
        if (!string.IsNullOrEmpty(resourceConstraints))
            args += $" --constraints \"{resourceConstraints}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Manages Docker Swarm networks for Aspire applications")]
    public static async Task<string> ManageDockerSwarmNetworkAsync(
        [Description("Action (create,update,delete,list)")] string action,
        [Description("Network name")] string? networkName = null,
        [Description("Network driver (overlay,bridge,host)")] string? driver = null,
        [Description("Subnet configuration")] string? subnet = null,
        [Description("Network options (key=value,key2=value2)")] string? options = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"docker-swarm network {action}";
        
        if (!string.IsNullOrEmpty(networkName))
            args += $" \"{networkName}\"";
        if (!string.IsNullOrEmpty(driver))
            args += $" --driver {driver}";
        if (!string.IsNullOrEmpty(subnet))
            args += $" --subnet \"{subnet}\"";
        if (!string.IsNullOrEmpty(options))
            args += $" --options \"{options}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Configures service mesh integration for Aspire applications")]
    public static async Task<string> ConfigureServiceMeshAsync(
        [Description("Service mesh type (istio,linkerd,consul-connect)")] string meshType,
        [Description("Application name")] string applicationName,
        [Description("Namespace")] string namespace_,
        [Description("Enable mutual TLS")] bool enableMtls = true,
        [Description("Traffic policy configuration")] string? trafficPolicy = null,
        [Description("Observability configuration")] string? observabilityConfig = null,
        [Description("Security policy configuration")] string? securityPolicy = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"service-mesh configure --type {meshType} --app \"{applicationName}\" --namespace \"{namespace_}\"";
        
        if (enableMtls)
            args += " --enable-mtls";
        if (!string.IsNullOrEmpty(trafficPolicy))
            args += $" --traffic-policy \"{trafficPolicy}\"";
        if (!string.IsNullOrEmpty(observabilityConfig))
            args += $" --observability \"{observabilityConfig}\"";
        if (!string.IsNullOrEmpty(securityPolicy))
            args += $" --security-policy \"{securityPolicy}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Manages ingress configuration for Aspire applications")]
    public static async Task<string> ManageIngressConfigurationAsync(
        [Description("Action (create,update,delete,get)")] string action,
        [Description("Ingress name")] string ingressName,
        [Description("Kubernetes context")] string? context = null,
        [Description("Namespace")] string? namespace_ = null,
        [Description("Host name")] string? hostName = null,
        [Description("Service name")] string? serviceName = null,
        [Description("Service port")] int? servicePort = null,
        [Description("TLS configuration")] string? tlsConfig = null,
        [Description("Ingress class")] string? ingressClass = null,
        [Description("Annotations (key=value,key2=value2)")] string? annotations = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"ingress {action} \"{ingressName}\"";
        
        if (!string.IsNullOrEmpty(context))
            args += $" --context \"{context}\"";
        if (!string.IsNullOrEmpty(namespace_))
            args += $" --namespace \"{namespace_}\"";
        if (!string.IsNullOrEmpty(hostName))
            args += $" --host \"{hostName}\"";
        if (!string.IsNullOrEmpty(serviceName))
            args += $" --service \"{serviceName}\"";
        if (servicePort.HasValue)
            args += $" --port {servicePort}";
        if (!string.IsNullOrEmpty(tlsConfig))
            args += $" --tls \"{tlsConfig}\"";
        if (!string.IsNullOrEmpty(ingressClass))
            args += $" --class \"{ingressClass}\"";
        if (!string.IsNullOrEmpty(annotations))
            args += $" --annotations \"{annotations}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Manages persistent volumes for Aspire applications")]
    public static async Task<string> ManagePersistentVolumeAsync(
        [Description("Action (create,delete,list,get)")] string action,
        [Description("Volume name")] string? volumeName = null,
        [Description("Kubernetes context")] string? context = null,
        [Description("Namespace")] string? namespace_ = null,
        [Description("Storage class")] string? storageClass = null,
        [Description("Volume size (e.g., 10Gi)")] string? size = null,
        [Description("Access modes (ReadWriteOnce,ReadOnlyMany,ReadWriteMany)")] string? accessModes = null,
        [Description("Mount path")] string? mountPath = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"pv {action}";
        
        if (!string.IsNullOrEmpty(volumeName))
            args += $" \"{volumeName}\"";
        if (!string.IsNullOrEmpty(context))
            args += $" --context \"{context}\"";
        if (!string.IsNullOrEmpty(namespace_))
            args += $" --namespace \"{namespace_}\"";
        if (!string.IsNullOrEmpty(storageClass))
            args += $" --storage-class \"{storageClass}\"";
        if (!string.IsNullOrEmpty(size))
            args += $" --size {size}";
        if (!string.IsNullOrEmpty(accessModes))
            args += $" --access-modes \"{accessModes}\"";
        if (!string.IsNullOrEmpty(mountPath))
            args += $" --mount-path \"{mountPath}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Configures cluster-wide policies for Aspire applications")]
    public static async Task<string> ConfigureClusterPoliciesAsync(
        [Description("Policy type (network,security,resource,rbac)")] string policyType,
        [Description("Policy name")] string policyName,
        [Description("Kubernetes context")] string context,
        [Description("Policy configuration file")] string? policyFile = null,
        [Description("Target namespace")] string? targetNamespace = null,
        [Description("Policy rules (inline JSON)")] string? policyRules = null,
        [Description("Enforcement mode (enforce,warn,dry-run)")] string? enforcementMode = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"cluster-policy configure --type {policyType} --name \"{policyName}\" --context \"{context}\"";
        
        if (!string.IsNullOrEmpty(policyFile))
            args += $" --policy-file \"{policyFile}\"";
        if (!string.IsNullOrEmpty(targetNamespace))
            args += $" --namespace \"{targetNamespace}\"";
        if (!string.IsNullOrEmpty(policyRules))
            args += $" --rules \"{policyRules}\"";
        if (!string.IsNullOrEmpty(enforcementMode))
            args += $" --enforcement {enforcementMode}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }
}

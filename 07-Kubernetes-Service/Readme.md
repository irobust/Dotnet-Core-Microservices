# Kubernetes commnad
## Moving from docker to kubernetes
* https://kompose.io
* kompose convert
* kompose convert -f docker-compose.yml
* kompose convert --out .k8s (create directory first)
* kompose convert --replicas=3

## Hello World
We will run one of the most common Docker helloworld applications out there- https://hub.docker.com/r/karthequian/helloworld/

### Kind Installation
* https://kind.sigs.k8s.io/docs/user/quick-start/
* kind create cluster --name kind
* kind get clusters
* kubectl cluster-info --context kind-kind
* kind delete cluster
* kind create cluster --config kind-config.yaml

### Web-based Kubernetes user interface
* kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/v2.3.1/aio/deploy/recommended.yaml
* kubectl proxy
* http://localhost:8001/api/v1/namespaces/kubernetes-dashboard/services/https:kubernetes-dashboard:/proxy/#/login
* kubectl describe secret -n kube-system
* Login with service account token

### Running your first helloworld
* kubectl cluster-info
* kubectl get nodes
* kubectl get all
* kubectl run [pod-name] --image=karthequian/helloworld --port=80
* kubectl create deploy [deployment-name] --image=karthequian/helloworld --port=80
* kubectl apply -f helloworld.yml
* kubectl expose deploy/[deployment-name] --type=NodePort
* kubectl get svc `Access helloworld application from http://localhost:[random-nodeport]`
* kubectl describe [deployment-name]
* kubectl describe [pod-name] `po/hw-5f6c6f9545-8hwp8`
* kubectl port-forward [pod-name] 8080:80

### Type of Service
* NodePort `flag exposes the deployment outside of the cluster by random port from 30000-32767`
* LoadBalancer `On cloud providers that support load balancers, an external IP address would be provisioned to access the service`
* ClusterIP `flag is only accessible by its internal IP address within the cluster`

### Scale your helloworld application
* kubectl scale --replicas=3 [deployment-name] `scale up`
* kubectl scale --replicas=1 [deployment-name] `scale down`

###  Troubleshooting
* kubectl logs <pod_name>
* kubectl exec -it [pod-name] /bin/bash `If you have only one contianer`
* kubectl exec -it [pod-name] -c [container-name] /bin/bash
* kubectl run -it net-debug --image=nixery.dev/shell/curl/wget/htop /bin/bash
* https://kubernetes.io/docs/tasks/debug-application-cluster/debug-running-pod/


#### Read more
* https://kubernetes.io/docs/tasks/access-application-cluster/web-ui-dashboard
* https://github.com/kubernetes/dashboard

# Kubernetes API Server
### Get definition references
* kubectl explain pods
* kubectl explain pod.spec
* kubectl explain pod.spec.containers
* kubectl api-resources --api-group=apps
* kubectl api-resources --namespaced=false
* kubectl explain deployment | head
* kubectl explain deployment --api-version apps/v1beta2 | head
* kubectl explain deployment --api-version apps/v1 | head
* kubectl api-versions | sort | more

### Basic concept API Object and API Server
* kubectl proxy --port 8888
* kubectl get po -v 9 `Track request and response`
* kubectl auth can-i get po
* curl http://localhost:8001/api/v1/namespaces/default/pods/hello-world
* curl http://localhost:8001/api/v1/namespaces/default/pods/hello-world/log 

# Kubernetes Storage
## Create a Persistent Volume(PV)
### Disable default storage class
* kubectl get sc
* kubectl edit sc/hostpath
* Set is-default-class to false (default is true)
```
storageclass.kubernetes.io/is-default-class: "false"
```
* kubectl apply -f Persistent-Volume/mongo.yaml
* kubectl get po -w
* kubectl apply -f Persistent-Volume/pvc.yaml
* kubectl apply -f Persistent-Volume/pv.yaml


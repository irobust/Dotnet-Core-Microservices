# Kubernetes Ingress
## Service Proxy with Ingress
What's ingress?
https://kubernetes.io/docs/concepts/services-networking/ingress/

Ingress Comparison
https://medium.com/flant-com/comparing-ingress-controllers-for-kubernetes-9b397483b46b

First, we need to add Contour to our cluster.
https://github.com/heptio/contour#add-contour-to-your-cluster

and install Contour with:

* kubectl apply -f https://projectcontour.io/quickstart/contour.yaml
* kubectl apply -f wishlist-contour.yaml
* kubectl get ing
* Consume service from http://localhost/products

Nginx Controller
* kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v0.40.2/deploy/static/provider/cloud/deploy.yaml
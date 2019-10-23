# ConsulForNetCore2.2-WebApi
 Base net core 2.x and consul , create demo webapi  to implementing service discovery

本地模拟测试使用docker 部署consul集群，并且测试API站点挂在本地IIS上

部署相关:
安装docker (windows)

docker pull consul

// Server  节点 1
docker run --name cs1 -p 8500:8500  -v /d/Document/Docker/data/cs1:/data consul agent -server -bind 172.17.0.2 -node consul-server-1  -data-dir /data -bootstrap-expect 3 -client 0.0.0.0 -ui

// Server  节点 2
docker run --name cs2 -p 7500:8500  -v /d/Document/Docker/data/cs2:/data consul agent -server -bind 172.17.0.3 -node consul-server-2  -data-dir /data -bootstrap-expect 3 -client 0.0.0.0 -ui -join 172.17.0.2

// Server  节点 3
docker run --name cs3 -p 6500:8500 -v /d/Document/Docker/data/cs3:/data consul agent -server -bind 172.17.0.4 -node consul-server-3  -data-dir /data -bootstrap-expect 3 -client 0.0.0.0 -ui -join 172.17.0.2

// Client 节点 1
docker run --name cc1 -p 5500:8500 -v /d/Document/Docker/data/cc1:/data consul agent -bind 172.17.0.5 -node consul-client-1 -data-dir /data -client 0.0.0.0 -ui -join 172.17.0.2

PS: 
每个节点开启之后，会自动寻找集群几点 刷新窗口，此时重新开一个powershell执行下个命令即可.
-v /d/Document/Docker/data/cs1:/data为本机目录，根据实际情况调整

参数名	解释
--name	Docker 容器名称（每个 Consul 节点一个容器）
-p	容器内部 8500 端口映射到当前主机端口，因为使用的同一台主机，所以这里每个容器内的 8500 端口映射到当前主机的不同端口
-v	将节点相关注册数据挂载到当前主机的指定位置，否则重启后会丢失
-server	设置为 Server 类型节点，不加则为 Client 类型节点
-bind	指定节点绑定的地址
-node	指定节点名称
-data-dir	数据存放位置
-bootstrap-expect	集群期望的 Server 节点数，只有达到这个值才会选举 Leader
-client	注册或者查询等一系列客户端对它操作的IP，默认是127.0.0.1
-ui	启用 UI 界面
-join	指定要加入的节点地址（组建集群）



查看节点状态和类型
docker exec -t cs1 consul members

查看server节点类型
docker exec -t cs1 consul operator raft list-peers


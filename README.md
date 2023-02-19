# dapr-workflow-sample
Dapr workflow sample (only for experiments)

# usage

## k8s (under construction...)

```bash
$ kind create cluster
$ skaffold run
```

## Dapr CLI

```bash
$ cd worker/worker
$ dapr run --app-id myworker --dapr-http-port 3501 dotnet run
```

```bash
$ curl -XPOST http://localhost:3501/v1.0-alpha1/workflows/dapr/BookingTripWorkflow/1234/start -d '{"input": {"City": "Tokyo", "Day": "Sunday", "Person": 2}}' -H "Content-Type: application/json"                                                                                                                            {"instance_id":"1234"}
$ curl http://localhost:3501/v1.0-alpha1/workflows/dapr/BookingTripWorkflow/1234 | jq
{
  "WFInfo": {
    "instance_id": "1234"
  },
  "start_time": "2023-02-19T09:00:02Z",
  "metadata": {
    "dapr.workflow.custom_status": "",
    "dapr.workflow.input": "{\"City\":\"Tokyo\",\"Day\":\"Sunday\",\"Person\":2}",
    "dapr.workflow.last_updated": "2023-02-19T09:00:03Z",
    "dapr.workflow.name": "BookingTripWorkflow",
    "dapr.workflow.output": "{\"Processed\":true,\"Price\":30000}",
    "dapr.workflow.runtime_status": "COMPLETED"
  }
}
```

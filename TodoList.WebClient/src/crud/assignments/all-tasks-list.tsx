import { useMutation, useQuery, useQueryClient } from "react-query";
import { httpClient } from "../../axios";
import { Button, Card, Flex, Typography } from "antd";
import { Employee } from "../employees/use-employees-list";

type Task = {
  id: string;
  title: string;
  text: string;
  created: Date;
  employee: Employee;
};

export function AllTasksList() {
  const allQuery = useQuery<Task[]>({
    queryKey: ["all-tasks"],
    queryFn: async () => (await httpClient.get("/task/getAll")).data,
  });

  const renderBody = () => {
    if (!allQuery.data?.length) {
      return <Typography.Text>Нет задач</Typography.Text>;
    }

    return (
      <Flex vertical gap={12}>
        {allQuery.data?.map((x) => {
          return <TaskCardView key={x.id} task={x} />;
        })}
      </Flex>
    );
  };

  return (
    <div>
      <Typography.Title level={2}>Список задач</Typography.Title>
      {renderBody()}
    </div>
  );
}

type TaskCardViewProps = {
  task: Task;
};

function TaskCardView(props: TaskCardViewProps) {
  const queryClient = useQueryClient();

  const deleteMutation = useMutation({
    mutationFn: () => httpClient.get("/task/delete/" + props.task.id),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["all-tasks"],
      });
    },
  });

  return (
    <Card>
      <Flex vertical gap={12}>
        <div>
          <Typography.Title level={5}>Название</Typography.Title>
          <Typography.Text>{props.task.title}</Typography.Text>
        </div>
        <div>
          <Typography.Title level={5}>Текст</Typography.Title>
          <Typography.Text>{props.task.text}</Typography.Text>
        </div>
        <div>
          <Typography.Title level={5}>Ответственный</Typography.Title>
          <Typography.Text>{props.task.employee.name}</Typography.Text>
        </div>
        <Typography.Text type="secondary">id = {props.task.id}</Typography.Text>
        <Typography.Text>
          {new Date(props.task.created).toLocaleString()}
        </Typography.Text>
        <Button onClick={() => deleteMutation.mutate()}>Удалить</Button>
      </Flex>
    </Card>
  );
}

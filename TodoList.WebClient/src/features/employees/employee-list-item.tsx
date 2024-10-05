import { Card, Flex, Typography, Button, Input } from "antd";
import { Employee } from "./use-employees-list";
import { useMutation, useQueryClient } from "react-query";
import { httpClient } from "../../axios";
import { useImmer } from "use-immer";
import { useState } from "react";

type Props = {
  employee: Employee;
};

export function EmployeeListItem(props: Props) {
  const queryClient = useQueryClient();

  const deleteMutation = useMutation({
    mutationFn: (id: string) => httpClient.get("/employee/delete/" + id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["employees"] });
      queryClient.invalidateQueries({ queryKey: ["all-tasks"] });
    },
  });

  const editMutation = useMutation({
    mutationFn: (employee: Employee) =>
      httpClient.post("/employee/update/", employee),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["employees"] });
    },
  });

  const [employee, updateEmployee] = useImmer(props.employee);
  const [state, setState] = useState<"edit" | "view">("view");

  if (state === "edit") {
    return (
      <>
        <Card>
          <Flex vertical gap={12}>
            <div>
              <Typography.Title level={5}>Имя</Typography.Title>
              <Input
                value={employee.name}
                onChange={(ev) => {
                  updateEmployee((draft) => {
                    draft.name = ev.target.value;
                  });
                }}
              />
            </div>
            <Flex gap={8}>
              <Button
                type="dashed"
                onClick={() => {
                  editMutation.mutate(employee);
                  setState("view");
                }}
              >
                Сохранить
              </Button>
              <Button type="dashed" onClick={() => setState("view")}>
                Отмена
              </Button>
            </Flex>
          </Flex>
        </Card>
      </>
    );
  }

  return (
    <>
      <Card>
        <Flex vertical gap={12}>
          <div>
            <Typography.Title level={5}>Имя</Typography.Title>
            <Typography.Text>{props.employee.name}</Typography.Text>
          </div>
          <Flex gap={8}>
            <Button
              type="dashed"
              onClick={() => deleteMutation.mutate(props.employee.id)}
            >
              Удалить
            </Button>
            <Button type="dashed" onClick={() => setState("edit")}>
              Редактировать
            </Button>
          </Flex>
        </Flex>
      </Card>
    </>
  );
}

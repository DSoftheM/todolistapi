import { useState } from "react";
import { useMutation, useQueryClient } from "react-query";
import { httpClient } from "../axios";

export function CreateTask() {
  const qc = useQueryClient();

  const createMutation = useMutation({
    mutationFn: () => httpClient.post("/task/create", { name, description }),
    onSuccess: () => {
      qc.invalidateQueries({
        queryKey: ["all-tasks"],
      });
    },
  });

  const [name, setName] = useState("");
  const [description, setDescription] = useState("");

  return (
    <div>
      <label>
        <p>Name</p>
        <input
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
      </label>
      <label>
        <p>Description</p>
        <input
          type="text"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
      </label>
      <button onClick={() => createMutation.mutate()}>Create task</button>
    </div>
  );
}

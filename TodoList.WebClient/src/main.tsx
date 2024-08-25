import { createRoot } from "react-dom/client";
import App from "./App.tsx";
import { QueryClient, QueryClientProvider } from "react-query";

const qc = new QueryClient();

createRoot(document.getElementById("root")!).render(
  <QueryClientProvider client={qc}>
    <App />
  </QueryClientProvider>
);

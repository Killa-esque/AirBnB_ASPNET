import { useRouteCustom } from "./hooks"

function App() {
  const routes = useRouteCustom();

  return (
    <>
      {routes}
    </>
  )
}

export default App

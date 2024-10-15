import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { Provider } from 'react-redux';
import { unstable_HistoryRouter as HistoryRouter } from 'react-router-dom';
import { createBrowserHistory } from 'history';
import App from './App.tsx'
import './index.css'
import { store } from './redux/configureStore.tsx';


export const history: any = createBrowserHistory();

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <HistoryRouter history={history}>
      <Provider store={store}>
        <App />
      </Provider>
    </HistoryRouter>
  </StrictMode>,
)

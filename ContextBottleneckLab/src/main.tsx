import { createRoot } from 'react-dom/client'
import App from './App'
import './style.css'

// StrictMode is intentionally omitted so this profiling exercise has no
// development-only double invocations. No React Compiler is enabled either.
createRoot(document.getElementById('root')!).render(<App />)

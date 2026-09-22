import { useState } from 'react'
import { useEventSocketBuggy } from './hooks/useEventSocketBuggy'
import { useEventSocket } from './hooks/useEventSocket'
import './App.css'

function App() {
  const [count, setCount] = useState(0)

  const buggyValue = useEventSocketBuggy(count)
  const fixedValue = useEventSocket(count)

  return (
    <>
      <section id="center">
        <div>
          <h1>Stale Closure Lab</h1>
          <p>
            React state, closures and <code>useRef</code>
          </p>
        </div>

        <div className="state">
          Current state: <strong>{count}</strong>
        </div>

        <button
          type="button"
          className="counter"
          onClick={() => setCount((count) => count + 1)}
        >
          Increase state
        </button>
      </section>

      <div className="ticks"></div>

      <section id="results">
        <div className="result-card">
          <div className="card-label">BUGGY</div>

          <h2>Stale closure</h2>

          <p>
            Socket sees:{' '}
            <strong>{buggyValue}</strong>
          </p>

          <span>
            The callback keeps the old state value.
          </span>
        </div>

        <div className="result-card">
          <div className="card-label">FIXED</div>

          <h2>useRef</h2>

          <p>
            Socket sees:{' '}
            <strong>{fixedValue}</strong>
          </p>

          <span>
            The callback reads the latest value from ref.
          </span>
        </div>
      </section>

      <div className="ticks"></div>

      <section id="explanation">
        <div>
          <h2>What is happening?</h2>

          <p>
            The state changes on every button click, but the
            socket subscription is created only once.
          </p>
        </div>

        <div>
          <h2>Goal</h2>

          <p>
            Keep one subscription while still reading the
            latest React state.
          </p>
        </div>
      </section>

      <div id="spacer"></div>
    </>
  )
}

export default App
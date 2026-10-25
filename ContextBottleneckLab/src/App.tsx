import { Profiler, useState } from 'react'
import Baseline from './Baseline'
import Optimized from './Optimized'
import PerformanceMetrics from './PerformanceMetrics'
import { createMetrics, MetricsContext } from './metrics'

function Session({ optimized }: { optimized: boolean }) {
  const [metrics] = useState(createMetrics)
  return <>
    <PerformanceMetrics metrics={metrics} />
    <h2>600 Rows</h2>
    <MetricsContext.Provider value={metrics}>
      <Profiler id={optimized ? 'optimized' : 'baseline'} onRender={metrics.onRender}>
        {optimized ? <Optimized /> : <Baseline />}
      </Profiler>
    </MetricsContext.Provider>
  </>
}

export default function App() {
  const [optimized, setOptimized] = useState(false)
  return <main>
    <h1>Context Bottleneck &amp; Component Isolation</h1>
    <h2>Implementation</h2>
    <div role="group" aria-label="Implementation" className="toggle">
      <button aria-pressed={!optimized} onClick={() => setOptimized(false)}>Baseline</button>
      <button aria-pressed={optimized} onClick={() => setOptimized(true)}>Optimized</button>
    </div>
    <Session key={String(optimized)} optimized={optimized} />
  </main>
}

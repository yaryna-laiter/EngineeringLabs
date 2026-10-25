import { useSyncExternalStore } from 'react'
import type { createMetrics } from './metrics'

export default function PerformanceMetrics({ metrics }: { metrics: ReturnType<typeof createMetrics> }) {
  const result = useSyncExternalStore(metrics.subscribe, metrics.getSnapshot)
  return <section aria-label="Performance Metrics">
    <h2>Performance Metrics</h2>
    <p>{result.phase === 'mount' ? 'Initial rows rendered' : 'Row re-renders (latest click)'}: <strong data-testid="render-count">{result.rows}</strong></p>
    <p>Render execution time: <strong>{result.duration.toFixed(2)} ms</strong></p>
    <p className="note">Initial mount renders all 600 rows. Time measures React rendering, not total click latency.</p>
  </section>
}

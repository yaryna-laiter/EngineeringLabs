import { createContext, useContext, useLayoutEffect } from 'react'
import type { ProfilerOnRenderCallback } from 'react'

export function createMetrics() {
  let rows = 0
  let snapshot = { rows: 0, duration: 0, phase: 'mount' }
  const listeners = new Set<() => void>()
  const onRender: ProfilerOnRenderCallback = (_id, phase, actualDuration) => {
    snapshot = { rows, duration: actualDuration, phase }
    rows = 0
    listeners.forEach(listener => listener())
  }
  return {
    onRender,
    markRow: () => { rows++ },
    getSnapshot: () => snapshot,
    subscribe: (listener: () => void) => {
      listeners.add(listener)
      return () => { listeners.delete(listener) }
    },
  }
}

// This Context value never changes, so instrumentation does not notify rows.
export const MetricsContext = createContext<ReturnType<typeof createMetrics> | null>(null)

export function useRowMeasurement() {
  const metrics = useContext(MetricsContext)
  // Count committed renders, before the enclosing Profiler callback runs.
  useLayoutEffect(() => { metrics?.markRow() })
}

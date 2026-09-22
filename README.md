# Engineering Labs

A collection of small practical labs focused on understanding runtime behavior, execution mechanics, and diagnostics in .NET and React.

## Labs

### 1. Async Deadlock & Thread Pool Starvation (.NET)

Demonstrates the impact of blocking asynchronous code with `.Result` under concurrent load.

The lab compares:

- Blocking async code
- Fully asynchronous `async/await`
- Thread Pool behavior under load
- Performance before and after the fix

Tools used include load testing and .NET diagnostics.

---

### 2. Memory Leak Hunt (.NET)

Demonstrates an intentional memory leak and how to diagnose object retention using memory profiling tools.

The lab covers:

- Creating an intentional memory leak
- Simulating application load
- Inspecting GC roots and object retention
- Fixing the leak
- Comparing memory usage before and after the fix

---

### 3. Stale Closure & Custom Hook Challenge (React)

Demonstrates how JavaScript closures interact with React renders and long-lived subscriptions.

The lab contains:

- A deliberately broken `useEventSocket` hook
- A visible stale closure bug
- A fixed implementation using `useRef`
- A stable subscription that can still access the latest React state

The mock socket is implemented using `setInterval`.

---

## Goal

Build practical knowledge of:

- Async execution and Thread Pool behavior
- Garbage Collection and memory diagnostics
- JavaScript lexical scope and closures
- React component lifecycle and hooks
- Debugging and profiling runtime problems

Each lab includes a small demo showing the problem, investigation, fix, and resulting behavior.

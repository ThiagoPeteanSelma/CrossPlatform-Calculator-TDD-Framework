import { useMemo, useState } from "react";

type Operation = "Add" | "Subtract" | "Multiply" | "Divide";

type CalculationResponse = {
  success: boolean;
  result?: number;
  formattedResult?: string;
  errorMessage?: string;
};

const operationLabels: Record<Operation, string> = {
  Add: "+",
  Subtract: "-",
  Multiply: "*",
  Divide: "/"
};

const apiBaseUrl = import.meta.env.VITE_API_BASE_URL ?? "https://localhost:7142";

function sanitizeNumericInput(raw: string): string {
  return raw.replace(/[^0-9+\-.,]/g, "");
}

function parseDecimal(raw: string): number | null {
  if (!raw.trim()) {
    return null;
  }

  const normalized = raw.replace(",", ".");
  const value = Number(normalized);

  return Number.isFinite(value) ? value : null;
}

export default function App() {
  const [leftOperandInput, setLeftOperandInput] = useState("0");
  const [rightOperandInput, setRightOperandInput] = useState("0");
  const [operation, setOperation] = useState<Operation>("Add");
  const [result, setResult] = useState<string>("");
  const [error, setError] = useState<string>("");
  const [isLoading, setIsLoading] = useState(false);

  const canSubmit = useMemo(() => {
    return parseDecimal(leftOperandInput) !== null && parseDecimal(rightOperandInput) !== null;
  }, [leftOperandInput, rightOperandInput]);

  async function calculate(): Promise<void> {
    const leftOperand = parseDecimal(leftOperandInput);
    const rightOperand = parseDecimal(rightOperandInput);

    if (leftOperand === null || rightOperand === null) {
      setError("Invalid numeric values.");
      setResult("");
      return;
    }

    setIsLoading(true);
    setError("");

    try {
      const response = await fetch(`${apiBaseUrl}/api/calculations`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({
          leftOperand,
          rightOperand,
          operation
        })
      });

      const payload = (await response.json()) as CalculationResponse;

      if (!response.ok || !payload.success) {
        setError(payload.errorMessage ?? "Calculation failed.");
        setResult("");
        return;
      }

      setResult(payload.formattedResult ?? String(payload.result ?? ""));
    } catch {
      setError("Unable to reach API.");
      setResult("");
    } finally {
      setIsLoading(false);
    }
  }

  return (
    <main className="page">
      <section className="card">
        <h1>Cross-Platform Calculator</h1>
        <p className="subtitle">Web vertical slice connected to the API</p>

        <div className="row">
          <label htmlFor="leftOperand">Left</label>
          <input
            id="leftOperand"
            name="leftOperand"
            value={leftOperandInput}
            onChange={(event) => setLeftOperandInput(sanitizeNumericInput(event.target.value))}
            inputMode="decimal"
          />
        </div>

        <div className="row">
          <label htmlFor="operation">Operation</label>
          <select
            id="operation"
            name="operation"
            value={operation}
            onChange={(event) => setOperation(event.target.value as Operation)}
          >
            {(Object.keys(operationLabels) as Operation[]).map((key) => (
              <option key={key} value={key}>
                {operationLabels[key]}
              </option>
            ))}
          </select>
        </div>

        <div className="row">
          <label htmlFor="rightOperand">Right</label>
          <input
            id="rightOperand"
            name="rightOperand"
            value={rightOperandInput}
            onChange={(event) => setRightOperandInput(sanitizeNumericInput(event.target.value))}
            inputMode="decimal"
          />
        </div>

        <button type="button" onClick={calculate} disabled={!canSubmit || isLoading}>
          {isLoading ? "Calculating..." : "="}
        </button>

        {result && (
          <output className="result" aria-label="result">
            Result: {result}
          </output>
        )}

        {error && <p className="error">{error}</p>}
      </section>
    </main>
  );
}

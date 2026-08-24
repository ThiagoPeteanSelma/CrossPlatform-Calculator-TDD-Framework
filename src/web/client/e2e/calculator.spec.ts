import { expect, test } from "@playwright/test";

test("adds two values through the UI", async ({ page }) => {
  await page.route("**/api/calculations", async (route) => {
    await route.fulfill({
      status: 200,
      contentType: "application/json",
      body: JSON.stringify({
        success: true,
        result: 12,
        formattedResult: "12"
      })
    });
  });

  await page.goto("/");
  await page.getByLabel("Left").fill("7");
  await page.getByLabel("Right").fill("5");
  await page.getByRole("button", { name: "=" }).click();

  await expect(page.getByLabel("result")).toContainText("12");
});

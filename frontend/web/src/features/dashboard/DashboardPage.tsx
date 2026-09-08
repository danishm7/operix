import {
  Activity,
  AlertTriangle,
  ArrowUpRight,
  CheckCircle2,
  CircleDashed,
  Clock3,
  Factory,
  PackageCheck,
  TrendingUp,
} from "lucide-react";

const cards = [
  {
    label: "Asset health",
    value: "92%",
    change: "+4.2%",
    detail: "vs last month",
    tint: "bg-emerald-50 text-emerald-700",
    icon: Activity,
  },
  {
    label: "Open work orders",
    value: "28",
    change: "-6",
    detail: "this week",
    tint: "bg-amber-50 text-amber-700",
    icon: CircleDashed,
  },
  {
    label: "PM compliance",
    value: "94%",
    change: "+3.1%",
    detail: "monthly target",
    tint: "bg-sky-50 text-sky-700",
    icon: CheckCircle2,
  },
  {
    label: "Spare parts",
    value: "1,240",
    change: "+18",
    detail: "inventory items",
    tint: "bg-violet-50 text-violet-700",
    icon: PackageCheck,
  },
];

const priorities = [
  {
    title: "Boiler #A-12",
    location: "Plant 1 • Utilities",
    due: "Due today",
    status: "Critical",
  },
  {
    title: "HVAC unit #C-04",
    location: "Plant 2 • HVAC",
    due: "Due in 2d",
    status: "High",
  },
  {
    title: "Conveyor #D-08",
    location: "Warehouse • Logistics",
    due: "Due in 4d",
    status: "Medium",
  },
];

const scheduleRows = [
  {
    asset: "Generator G-03",
    type: "Inspection",
    technician: "Ali Raza",
    date: "Aug 12",
    status: "Scheduled",
  },
  {
    asset: "Compressor K-09",
    type: "Lubrication",
    technician: "Samir Khan",
    date: "Aug 13",
    status: "In progress",
  },
  {
    asset: "Air handler H-02",
    type: "Filter replace",
    technician: "Nora Lee",
    date: "Aug 14",
    status: "Pending",
  },
  {
    asset: "Chiller C-17",
    type: "Calibration",
    technician: "Ravi Shah",
    date: "Aug 15",
    status: "Scheduled",
  },
];

const alerts = [
  { item: "Motor oil stock", qty: "14 units left", level: "Low" },
  { item: "Air filters", qty: "7 units left", level: "Critical" },
  { item: "Safety gloves", qty: "81 units left", level: "Healthy" },
];

function DashboardPage() {
  return (
    <div className="space-y-6">
      <div className="flex flex-col gap-4 lg:flex-row lg:items-center lg:justify-between">
        <div>
          <p className="text-sm font-medium uppercase tracking-[0.18em] text-slate-500">
            Operations overview
          </p>
          <h1 className="mt-2 text-3xl font-semibold tracking-tight text-slate-900">
            CMMS Dashboard
          </h1>
        </div>

        <div className="flex items-center gap-3">
          <button
            type="button"
            className="rounded-xl border border-slate-200 bg-white px-4 py-2 text-sm font-medium text-slate-700 transition-colors hover:bg-slate-50"
          >
            Export report
          </button>
          <button
            type="button"
            className="rounded-xl bg-slate-900 px-4 py-2 text-sm font-medium text-white transition-colors hover:bg-slate-800"
          >
            New work order
          </button>
        </div>
      </div>

      <div className="grid gap-4 md:grid-cols-2 xl:grid-cols-4">
        {cards.map(({ label, value, change, detail, tint, icon: Icon }) => (
          <div key={label} className="panel-surface p-5 soft-ring">
            <div className="flex items-start justify-between gap-3">
              <div>
                <p className="text-sm text-slate-500">{label}</p>
                <p className="mt-3 text-3xl font-semibold tracking-tight text-slate-900">
                  {value}
                </p>
              </div>

              <div className={`rounded-xl p-2.5 ${tint}`}>
                <Icon className="size-5" />
              </div>
            </div>

            <div className="mt-5 flex items-center justify-between text-sm">
              <span className="font-semibold text-emerald-600">{change}</span>
              <span className="text-slate-500">{detail}</span>
            </div>
          </div>
        ))}
      </div>

      <div className="grid gap-6 xl:grid-cols-[1.65fr_1fr]">
        <div className="panel-surface p-5 soft-ring">
          <div className="mb-5 flex items-center justify-between">
            <div>
              <h2 className="text-lg font-semibold text-slate-900">
                Priority work orders
              </h2>
              <p className="text-sm text-slate-500">
                Critical items requiring attention
              </p>
            </div>

            <button
              type="button"
              className="text-sm font-medium text-slate-600 hover:text-slate-900"
            >
              View all
            </button>
          </div>

          <div className="space-y-3">
            {priorities.map(({ title, location, due, status }) => (
              <div
                key={title}
                className="flex items-center justify-between rounded-2xl border border-slate-200 bg-slate-50 p-3"
              >
                <div className="flex items-center gap-3">
                  <div className="flex size-10 items-center justify-center rounded-xl bg-slate-900 text-white">
                    <Factory className="size-4" />
                  </div>

                  <div>
                    <p className="font-medium text-slate-800">{title}</p>
                    <p className="text-sm text-slate-500">{location}</p>
                  </div>
                </div>

                <div className="flex items-center gap-3">
                  <div className="text-right">
                    <p className="text-sm font-medium text-slate-700">{due}</p>
                    <p className="text-xs text-slate-500">Maintenance</p>
                  </div>

                  <span
                    className={[
                      "rounded-full px-2.5 py-1 text-xs font-semibold",
                      status === "Critical"
                        ? "bg-rose-100 text-rose-700"
                        : status === "High"
                          ? "bg-orange-100 text-orange-700"
                          : "bg-amber-100 text-amber-700",
                    ].join(" ")}
                  >
                    {status}
                  </span>
                </div>
              </div>
            ))}
          </div>
        </div>

        <div className="panel-surface p-5 soft-ring">
          <div className="mb-5 flex items-center justify-between">
            <div>
              <h2 className="text-lg font-semibold text-slate-900">
                Inventory alerts
              </h2>
              <p className="text-sm text-slate-500">Critical stock items</p>
            </div>
            <AlertTriangle className="size-5 text-amber-500" />
          </div>

          <div className="space-y-3">
            {alerts.map(({ item, qty, level }) => (
              <div
                key={item}
                className="flex items-center justify-between rounded-2xl border border-slate-200 bg-slate-50 p-3"
              >
                <div>
                  <p className="font-medium text-slate-800">{item}</p>
                  <p className="text-sm text-slate-500">{qty}</p>
                </div>

                <span
                  className={[
                    "rounded-full px-2 py-1 text-xs font-semibold",
                    level === "Critical"
                      ? "bg-rose-100 text-rose-700"
                      : level === "Low"
                        ? "bg-amber-100 text-amber-700"
                        : "bg-emerald-100 text-emerald-700",
                  ].join(" ")}
                >
                  {level}
                </span>
              </div>
            ))}
          </div>
        </div>
      </div>

      <div className="grid gap-6 xl:grid-cols-[1.35fr_0.95fr]">
        <div className="panel-surface p-5 soft-ring">
          <div className="mb-5 flex items-center justify-between">
            <div>
              <h2 className="text-lg font-semibold text-slate-900">
                Upcoming maintenance
              </h2>
              <p className="text-sm text-slate-500">
                Scheduled for the next 7 days
              </p>
            </div>
            <button
              type="button"
              className="flex items-center gap-1 text-sm font-medium text-slate-600 hover:text-slate-900"
            >
              View calendar
              <ArrowUpRight className="size-4" />
            </button>
          </div>

          <div className="overflow-hidden rounded-2xl border border-slate-200">
            <table className="min-w-full text-left text-sm">
              <thead className="bg-slate-50 text-slate-600">
                <tr>
                  <th className="px-4 py-3 font-medium">Asset</th>
                  <th className="px-4 py-3 font-medium">Type</th>
                  <th className="px-4 py-3 font-medium">Technician</th>
                  <th className="px-4 py-3 font-medium">Date</th>
                  <th className="px-4 py-3 font-medium">Status</th>
                </tr>
              </thead>

              <tbody>
                {scheduleRows.map(
                  ({ asset, type, technician, date, status }) => (
                    <tr
                      key={asset}
                      className="border-t border-slate-200 bg-white"
                    >
                      <td className="px-4 py-3 font-medium text-slate-800">
                        {asset}
                      </td>
                      <td className="px-4 py-3 text-slate-600">{type}</td>
                      <td className="px-4 py-3 text-slate-600">{technician}</td>
                      <td className="px-4 py-3 text-slate-600">{date}</td>
                      <td className="px-4 py-3">
                        <span
                          className={[
                            "rounded-full px-2 py-1 text-xs font-semibold",
                            status === "Scheduled"
                              ? "bg-sky-100 text-sky-700"
                              : status === "In progress"
                                ? "bg-violet-100 text-violet-700"
                                : "bg-amber-100 text-amber-700",
                          ].join(" ")}
                        >
                          {status}
                        </span>
                      </td>
                    </tr>
                  ),
                )}
              </tbody>
            </table>
          </div>
        </div>

        <div className="panel-surface p-5 soft-ring">
          <div className="mb-5 flex items-center justify-between">
            <div>
              <h2 className="text-lg font-semibold text-slate-900">
                Performance
              </h2>
              <p className="text-sm text-slate-500">This month</p>
            </div>
            <TrendingUp className="size-5 text-emerald-600" />
          </div>

          <div className="space-y-5">
            <div>
              <div className="mb-2 flex items-center justify-between text-sm text-slate-600">
                <span>Equipment uptime</span>
                <span className="font-semibold text-slate-800">96%</span>
              </div>
              <div className="h-2.5 rounded-full bg-slate-100">
                <div className="h-2.5 w-[96%] rounded-full bg-emerald-500" />
              </div>
            </div>

            <div>
              <div className="mb-2 flex items-center justify-between text-sm text-slate-600">
                <span>Work order completion</span>
                <span className="font-semibold text-slate-800">87%</span>
              </div>
              <div className="h-2.5 rounded-full bg-slate-100">
                <div className="h-2.5 w-[87%] rounded-full bg-sky-500" />
              </div>
            </div>

            <div>
              <div className="mb-2 flex items-center justify-between text-sm text-slate-600">
                <span>Preventive maintenance</span>
                <span className="font-semibold text-slate-800">94%</span>
              </div>
              <div className="h-2.5 rounded-full bg-slate-100">
                <div className="h-2.5 w-[94%] rounded-full bg-violet-500" />
              </div>
            </div>
          </div>

          <div className="mt-6 rounded-2xl border border-slate-200 bg-slate-50 p-4">
            <div className="flex items-center justify-between">
              <div className="flex items-center gap-2">
                <Clock3 className="size-4 text-slate-500" />
                <span className="text-sm text-slate-600">
                  Avg. response time
                </span>
              </div>
              <span className="text-lg font-semibold text-slate-900">2.4h</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default DashboardPage;

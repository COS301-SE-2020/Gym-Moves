window.onload = function () {

var chart = new CanvasJS.Chart("chartContainer", {
					animationEnabled: true,
	theme: "light2", // "light1", "light2", "dark1", "dark2"
	title:{
					text: "Gym Attendance"
	},
	axisY: {
					title: "Days of the week"
	},
	data: [{
					type: "column",
		showInLegend: true,
		legendMarkerColor: "grey",
		dataPoints: [
			{y: 300878, label: "Monday" },
			{y: 266455,  label: "Tuesday" },
			{y: 169709,  label: "Wednesday" },
			{y: 158400,  label: "Thursday" },
			{y: 142503,  label: "Friday" },
			{y: 101500, label: "Saturday" },
			{y: 97800,  label: "Sunday" }
		]
	}]
});
chart.render();

}
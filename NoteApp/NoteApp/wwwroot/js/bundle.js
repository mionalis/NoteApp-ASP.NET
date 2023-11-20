$("#edit-note, #delete-note").click(function (e) {
	var noteId = $("#notes-list-box").val();
	if (noteId == 0) {
		e.preventDefault();
	}
});

$("#notes-list-box").on("change", function () {
	var noteId = $("#notes-list-box").val();
	var categoryId = $("#note-category-combo-box").val();
	$.ajax({
		type: "Post",
		url: "/Note/ShowNote?id=" + noteId,
		contentType: "html",
		success: function (response) {
			$("#main-container").html(response);
			sc
			$("#note-category-combo-box").val(categoryId);
		}
	});
});

$("#note-category-combo-box").on("change", function () {
	var categoryId = $("#note-category-combo-box").val();
	$.ajax({
		type: "Post",
		url: "/Note/FilterNotes?id=" + categoryId,
		contentType: "html",
		success: function (response) {
			$("#main-container").html(response);
			$("#note-category-combo-box").val(categoryId);
		}
	});
});